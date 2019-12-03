package test.service.user;

import test.model.User;

import java.util.List;

public interface UserService {
    User findByUsername(String userName);
    List<User> getAll();
}
