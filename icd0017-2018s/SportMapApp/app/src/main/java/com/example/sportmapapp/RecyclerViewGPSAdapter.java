package com.example.sportmapapp;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.example.sportmapapp.Domain.Session;

import java.util.List;

public class RecyclerViewGPSAdapter extends RecyclerView.Adapter<RecyclerViewGPSAdapter.ViewHolder>  {

    private List<Session> sessions;
    private LayoutInflater inflater;
    private ItemClickListener itemClickListener;

    public RecyclerViewGPSAdapter(Context context, List<Session> sessions) {
        inflater = LayoutInflater.from(context);
        this.sessions = sessions;
    }

    // inflate the view from xml layout when needed
    @NonNull
    @Override
    public RecyclerViewGPSAdapter.ViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View rowView = inflater.inflate(R.layout.recycler_view_row, viewGroup, false);
        return new ViewHolder(rowView);
    }


    // binds the data to the view elements in each row
    @Override
    public void onBindViewHolder(@NonNull RecyclerViewGPSAdapter.ViewHolder viewHolder, int i) {
        viewHolder.textViewSessionIdData.setText(String.valueOf(sessions.get(i).id));
        viewHolder.textViewTotalDistanceData.setText(sessions.get(i).totalDistance);
        viewHolder.textViewTotalTimeData.setText(sessions.get(i).totalTime);
        viewHolder.textViewTotalPaceData.setText(sessions.get(i).pace);
    }

    @Override
    public int getItemCount() {
        // how many rows there will be
        return sessions.size();
    }

    public void updateData(List<Session> sessions){
        this.sessions = sessions;
        notifyDataSetChanged();
    }

    // stores and recycles views as they are scrolled off the screen
    public class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {

        TextView textViewSessionIdData;
        TextView textViewTotalDistanceData;
        TextView textViewTotalTimeData;
        TextView textViewTotalPaceData;
        Button buttonExport;
        Button buttonDelete;

        public ViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewSessionIdData = itemView.findViewById(R.id.textViewSessionIdData);
            textViewTotalDistanceData = itemView.findViewById(R.id.textViewTotalDistanceData);
            textViewTotalPaceData = itemView.findViewById(R.id.textViewPaceData);
            textViewTotalTimeData = itemView.findViewById(R.id.textViewTotalTimeData);
            itemView.setOnClickListener(this); //ViewHolder.onClick
            buttonExport = itemView.findViewById(R.id.buttonExport);
            buttonExport.setOnClickListener(this);
            buttonDelete = itemView.findViewById(R.id.buttonDelete);
            buttonDelete.setOnClickListener(this);
        }

        @Override
        public void onClick(View v) {
            if (itemClickListener != null){
                if (v.getId() == buttonExport.getId()){
                    itemClickListener.onButtonExportInRecyclerViewClick(v, getAdapterPosition());
                } else if (v.getId() == buttonDelete.getId()){
                    itemClickListener.onButtonDeleteInRecyclerViewClick(v, getAdapterPosition());
                } else {
                    itemClickListener.onStatisticsRecyclerViewRowClick(v, getAdapterPosition());
                }
            }
        }

    }

    void setItemClickListener(ItemClickListener itemClickListener){
        this.itemClickListener = itemClickListener;
    }

    public interface ItemClickListener{
        void onStatisticsRecyclerViewRowClick(View view, int position);
        void onButtonExportInRecyclerViewClick(View view, int position);
        void onButtonDeleteInRecyclerViewClick(View view, int position);

    }

}
