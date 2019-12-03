package test.service;

import java.util.List;

public interface BaseService<T> {
    T findById(long id);
    T save(T entity);
    List<T> getAll();
    void deleteById(long id);
}
