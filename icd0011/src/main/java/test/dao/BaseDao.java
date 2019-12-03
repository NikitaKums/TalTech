package test.dao;

import java.util.List;

public interface BaseDao<T> {
    T findById(long id);
    T save(T entity);
    List<T> getAll();
    void deleteById(long id);
}
