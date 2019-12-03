package test.app.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.PropertySource;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;
import test.model.User;
import test.service.user.UserService;

import java.util.List;

@RestController
@PropertySource("classpath:/application.properties")
public class UserController {

    @Value("${app.version}")
    private String appVersion;

    @Autowired
    private UserService userService;

    @GetMapping("/version")
    public String version(){
        return appVersion;
    }

    @GetMapping("/users/{userName}")
    @PreAuthorize("#userName == authentication.name OR hasRole('ADMIN')")
    public User getUserByName(@PathVariable String userName) {
        return userService.findByUsername(userName);
    }

    @GetMapping("/users")
    public List<User> getAllUsers(){
        return userService.getAll();
    }
}
