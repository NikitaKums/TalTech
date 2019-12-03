package test.dao.order;

import org.springframework.stereotype.Repository;
import test.model.Order;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;
import javax.transaction.Transactional;
import java.util.List;

@Repository
public class OrderDaoImpl implements OrderDao {

    @PersistenceContext
    private EntityManager em;

    @Transactional
    @Override
    public Order save(Order order){
        if (order.getId() == null){
            em.persist(order);
            return order;
        } else {
            em.merge(order);
            return order;
        }
    }

    @Override
    public List<Order> getAll() {
        TypedQuery<Order> query = em.createQuery(
                "SELECT DISTINCT o from Order o LEFT JOIN FETCH o.orderRows ORDER BY o.id",
                Order.class);
        return query.getResultList();
    }

    @Override
    public void deleteById(long id) {
        Order order = em.find(Order.class, id);
        if (order != null) {
            em.remove(order);
        }
    }

    @Override
    public Order findById(long id) {
        TypedQuery<Order> query = em.createQuery(
                "SELECT DISTINCT o FROM Order o " +
                        "LEFT JOIN FETCH o.orderRows " +
                        "WHERE o.id = :id", Order.class);
        query.setParameter("id", id);
        return query.getSingleResult();
    }
}
