import java.util.*;
//https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/
//https://stackoverflow.com/questions/8379785/how-does-a-breadth-first-search-work-when-looking-for-shortest-path
/** Container class to different classes, that makes the whole
 * set of classes one class formally.
 */
public class GraphTask {

   /**
    * Main method.
    */
   public static void main(String[] args) {
      GraphTask a = new GraphTask();
      a.run();
   }

   /**
    * Actual main method to run examples and everything.
    */
   public void run() {
      Graph g = new Graph("G");
      g.createRandomSimpleGraph(15, 14);
      System.out.println(g);
      System.out.println(g.findShortestPath("v1", "v12"));
   }

   /**
    * Vertex represents one node in the graph.
    * Connected via Arcs to other vertexes.
    */
   class Vertex {

      private String id;
      private Vertex next;
      private Arc first;
      private int info = 0;
      public boolean visited = false;
      Vertex parent = null;
      // You can add more fields, if needed

      Vertex(String s, Vertex v, Arc e) {
         id = s;
         next = v;
         first = e;
      }

      Vertex(String s) {
         this(s, null, null);
      }

      @Override
      public String toString() {
         return id;
      }

   }


   /**
    * Arc represents one arrow in the graph. Two-directional edges are
    * represented by two Arc objects (for both directions).
    */
   class Arc {

      private String id;
      private Vertex target;
      private Arc next;
      private int info = 0;
      // You can add more fields, if needed

      Arc(String s, Vertex v, Arc a) {
         id = s;
         target = v;
         next = a;
      }

      Arc(String s) {
         this(s, null, null);
      }

      @Override
      public String toString() {
         return id;
      }

   }


   class Graph {

      private String id;
      private Vertex first;
      private int info = 0;
      // You can add more fields, if needed

      Graph(String s, Vertex v) {
         id = s;
         first = v;
      }

      Graph(String s) {
         this(s, null);
      }

      @Override
      public String toString() {
         String nl = System.getProperty("line.separator");
         StringBuilder sb = new StringBuilder(nl);
         sb.append(id);
         sb.append(nl);
         Vertex v = first;
         while (v != null) {
            sb.append(v.toString());
            sb.append(" -->");
            Arc a = v.first;
            while (a != null) {
               sb.append(" ");
               sb.append(a.toString());
               sb.append(" (");
               sb.append(v.toString());
               sb.append("->");
               sb.append(a.target.toString());
               sb.append(")");
               a = a.next;
            }
            sb.append(nl);
            v = v.next;
         }
         return sb.toString();
      }

      public Vertex createVertex(String vid) {
         Vertex res = new Vertex(vid);
         res.next = first;
         first = res;
         return res;
      }

      public Arc createArc(String aid, Vertex from, Vertex to) {
         Arc res = new Arc(aid);
         res.next = from.first;
         from.first = res;
         res.target = to;
         return res;
      }

      /**
       * Create a connected undirected random tree with n vertices.
       * Each new vertex is connected to some random existing vertex.
       *
       * @param n number of vertices added to this graph
       */
      public void createRandomTree(int n) {
         if (n <= 0)
            return;
         Vertex[] varray = new Vertex[n];
         for (int i = 0; i < n; i++) {
            varray[i] = createVertex("v" + String.valueOf(n - i));
            if (i > 0) {
               int vnr = (int) (Math.random() * i);
               createArc("a" + varray[vnr].toString() + "_"
                       + varray[i].toString(), varray[vnr], varray[i]);
               createArc("a" + varray[i].toString() + "_"
                       + varray[vnr].toString(), varray[i], varray[vnr]);
            } else {
            }
         }
      }

      /**
       * Create an adjacency matrix of this graph.
       * Side effect: corrupts info fields in the graph
       *
       * @return adjacency matrix
       */
      public int[][] createAdjMatrix() {
         info = 0;
         Vertex v = first;
         while (v != null) {
            v.info = info++;
            v = v.next;
         }
         int[][] res = new int[info][info];
         v = first;
         while (v != null) {
            int i = v.info;
            Arc a = v.first;
            while (a != null) {
               int j = a.target.info;
               res[i][j]++;
               a = a.next;
            }
            v = v.next;
         }
         return res;
      }

      /**
       * Create a connected simple (undirected, no loops, no multiple
       * arcs) random graph with n vertices and m edges.
       *
       * @param n number of vertices
       * @param m number of edges
       */
      public void createRandomSimpleGraph(int n, int m) {
         if (n <= 0)
            return;
         if (n > 2500)
            throw new IllegalArgumentException("Too many vertices: " + n);
         if (m < n - 1 || m > n * (n - 1) / 2)
            throw new IllegalArgumentException
                    ("Impossible number of edges: " + m);
         first = null;
         createRandomTree(n);       // n-1 edges created here
         Vertex[] vert = new Vertex[n];
         Vertex v = first;
         int c = 0;
         while (v != null) {
            vert[c++] = v;
            v = v.next;
         }
         int[][] connected = createAdjMatrix();
         int edgeCount = m - n + 1;  // remaining edges
         while (edgeCount > 0) {
            int i = (int) (Math.random() * n);  // random source
            int j = (int) (Math.random() * n);  // random target
            if (i == j)
               continue;  // no loops
            if (connected[i][j] != 0 || connected[j][i] != 0)
               continue;  // no multiple edges
            Vertex vi = vert[i];
            Vertex vj = vert[j];
            createArc("a" + vi.toString() + "_" + vj.toString(), vi, vj);
            connected[i][j] = 1;
            createArc("a" + vj.toString() + "_" + vi.toString(), vj, vi);
            connected[j][i] = 1;
            edgeCount--;  // a new edge happily created
         }
      }

      /**
       * Find shortest path from one vertex to other vertex from graph.
       *
       * @param startingVertexName starting vertex name
       * @param endingVertexName ending vertex name
       * @return List of arcs that form the shortest path
       */
      public List<Arc> findShortestPath(String startingVertexName, String endingVertexName) {

         //empty name supplied
         if (startingVertexName.trim().isEmpty() || endingVertexName.trim().isEmpty()) {
            throw new RuntimeException("Empty vertex name supplied.");
            //System.out.println("Empty vertex name supplied.");
            //System.exit(0);
         }

         //check if starting vertex is the ending vertex
         if (startingVertexName.equals(endingVertexName)) {
            throw new RuntimeException("START vertex and END vertex have same names!");
            //System.out.println("START vertex and END vertex are the same! (Method was not executed)");
            //System.out.println(startingVertex + " -> " + endingVertex);
            //System.exit(0);
         }

         Vertex start = new Vertex(startingVertexName);
         Vertex end = new Vertex(endingVertexName);

         Vertex firstVertex = first;

         // get starting and ending vertex and validate vertexes from parameters
         Vertex startVertex = null;
         Vertex endVertex = null;

         // find vertex object that has the same id as given vertex names as parameters
         while (firstVertex != null) {
            if (firstVertex.id.equals(start.id)) {
               startVertex = firstVertex; // starting vertex
            }
            if (firstVertex.id.equals(end.id)) {
               endVertex = firstVertex; // ending vertex
            }
            firstVertex = firstVertex.next;
         }

         // vertexes do not exist in the graph
         if (startVertex == null || endVertex == null) {
            throw new RuntimeException("No such vertex named: " + (startVertex == null ? start.id : end.id));
            //System.out.println("No such vertex named: " + (startVertex == null ? start.id : end.id));
            //System.exit(0);
         }

         LinkedList<Vertex> queue = new LinkedList<>(); // queue for bfs

         startVertex.visited = true; // starting vertex is visited
         startVertex.parent = null;
         queue.add(startVertex); // add starting vertex to queue

         while (queue.size() != 0) {
            start = queue.poll();
            List<Vertex> connectedVertexes = getVertexVertices(start); // get all connected vertexes from current vertex
            for (Vertex vertex : connectedVertexes) {
               if (!vertex.visited) {
                  vertex.visited = true;
                  vertex.parent = start; // current 'parent' vertex is closest way to this vertex
                  queue.add(vertex);
               }
            }
         }

         List<Arc> path = new ArrayList<>(); // holds path as arc objects

         // each vertex (VERTEX) has a parent vertex that is the closest way of reaching VERTEX thus iterating over
         // parents will yield the closest way from end point till start point.
         Vertex temp = endVertex;
         while (true){
            if(temp.parent == null){
               break;
            }
            Arc arc = temp.first;
            while (arc.target != temp.parent){
               arc = arc.next;
            }
            path.add(getOtherEndOfArc(arc, temp));
            temp = temp.parent;
         }
         Collections.reverse(path);
         return path;
      }

      /**
       * Find all vertexes who are connected to given vertex.
       *
       * @param vertex Vertex whose 'children' are to be found
       * @return List of all connected vertexes with given vertex
       */
      private List<Vertex> getVertexVertices(Vertex vertex) {
         List<Vertex> result = new ArrayList<>();
         Arc temp = vertex.first;
         while (temp != null) {
            result.add(temp.target);
            temp = temp.next;
         }
         return result;
      }

      /**
       * Find reverse arc to given arc. av5_v6 -> av6_v5
       *
       * @param arc Arc that needs it's reverse to be found
       * @param end Vertex that the reversed arc must connect to
       * @return Arc that is reverse of given arc
       */
      private Arc getOtherEndOfArc(Arc arc, Vertex end){
         Arc result = arc.target.first;
         while (result.target != end){
            result = result.next;
         }

         return result;
      }

   }
}