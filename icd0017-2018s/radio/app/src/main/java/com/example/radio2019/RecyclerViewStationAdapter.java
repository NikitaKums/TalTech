package com.example.radio2019;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.radio2019.Domain.Station;

import java.util.List;

public class RecyclerViewStationAdapter extends RecyclerView.Adapter<RecyclerViewStationAdapter.ViewHolder> {
    private List<Station> stations;
    private LayoutInflater inflater;
    private ItemClickListener itemClickListener;


    public RecyclerViewStationAdapter(Context context, List<Station> stations) {
        this.stations = stations;
        inflater = LayoutInflater.from(context); // we need to unpack our xml layouts
    }

    // inflate the view from xml layout when needed
    @NonNull
    @Override
    public RecyclerViewStationAdapter.ViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View rowView = inflater.inflate(R.layout.recycler_station_row, viewGroup, false);
        return new ViewHolder(rowView);
    }

    // binds the data to the view elements in each row
    @Override
    public void onBindViewHolder(@NonNull RecyclerViewStationAdapter.ViewHolder viewHolder, int i) {
        viewHolder.textViewStationName.setText(stations.get(i).stationName);
    }

    @Override
    public int getItemCount() {
        // how many rows there will be
        return stations.size();
    }

    // stores and recycles views as they are scrolled off the screen
    public class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        TextView textViewStationName;

        public ViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewStationName = itemView.findViewById(R.id.textViewStationName);
            itemView.setOnClickListener(this); //ViewHolder.onClick
        }

        @Override
        public void onClick(View v) {
            if (itemClickListener != null){
                itemClickListener.onStationRecyclerViewRowClick(v, getAdapterPosition());
            }
        }

    }

    // whoever uses this recycler has to implement ItemClickListener interface and set itself up as our listener
    void setItemClickListener(ItemClickListener itemClickListener){
        this.itemClickListener = itemClickListener;
    }

    public interface ItemClickListener{
        void onStationRecyclerViewRowClick(View view, int position);
    }

}

