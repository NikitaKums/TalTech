package test.dao.user;

import test.model.User;

import java.util.List;

public interface UserDao {
    User findByUsername(String userName);
    List<User> getAll();
}
