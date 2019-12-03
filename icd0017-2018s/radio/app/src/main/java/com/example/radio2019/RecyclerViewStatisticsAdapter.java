package com.example.radio2019;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.radio2019.Domain.Artist;
import com.example.radio2019.Domain.Song;

import java.util.List;

public class RecyclerViewStatisticsAdapter extends RecyclerView.Adapter<RecyclerViewStatisticsAdapter.ViewHolder> {
    private List<Artist> artists;
    private LayoutInflater inflater;
    private ItemClickListener itemClickListener;

    public RecyclerViewStatisticsAdapter(Context context, List<Artist> artists) {
        this.artists = artists;
        inflater = LayoutInflater.from(context); // we need to unpack our xml layouts
    }

    // inflate the view from xml layout when needed
    @NonNull
    @Override
    public RecyclerViewStatisticsAdapter.ViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View rowView = inflater.inflate(R.layout.recycler_result_row, viewGroup, false);
        return new ViewHolder(rowView);
    }


    // binds the data to the view elements in each row
    @Override
    public void onBindViewHolder(@NonNull RecyclerViewStatisticsAdapter.ViewHolder viewHolder, int i) {
        viewHolder.textViewArtistName.setText(artists.get(i).artistName);
        String temp = "Songs total for this artist: " + String.valueOf(artists.get(i).songs.size());
        viewHolder.textViewSongsAmount.setText(temp);

        List<Song> artistSongs = artists.get(i).songs;

        for (Song songs : artistSongs){
            viewHolder.textViewSongName.append(songs.songTitle + "\n");
            viewHolder.textViewSongTimesPlayed.append("Played " + songs.timesPlayed + " time(s)\n");
        }
    }

    @Override
    public int getItemCount() {
        // how many rows there will be
        return artists.size();
    }

    // stores and recycles views as they are scrolled off the screen
    public class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        TextView textViewArtistName;
        TextView textViewSongsAmount;
        TextView textViewSongName;
        TextView textViewSongTimesPlayed;


        public ViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewArtistName = itemView.findViewById(R.id.textViewArtistName);
            textViewSongsAmount = itemView.findViewById(R.id.textViewSongsAmount);
            textViewSongName = itemView.findViewById(R.id.textViewSongName);
            textViewSongTimesPlayed = itemView.findViewById(R.id.textViewSongTimesPlayed);
            itemView.setOnClickListener(this); //ViewHolder.onClick
        }

        @Override
        public void onClick(View v) {
            if (itemClickListener != null){
                itemClickListener.onStatisticsRecyclerViewRowClick(v, getAdapterPosition());
            }
        }

    }

    // whoever uses this recycler has to implement ItemClickListener interface and set itself up as our listener
    void setItemClickListener(ItemClickListener itemClickListener){
        this.itemClickListener = itemClickListener;
    }

    public interface ItemClickListener{
        void onStatisticsRecyclerViewRowClick(View view, int position);
    }

}

