package test.dao.user;

import org.springframework.stereotype.Repository;
import test.model.User;

import javax.persistence.EntityManager;
import javax.persistence.NoResultException;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;
import java.util.List;

@Repository
public class UserDaoImpl implements UserDao {

    @PersistenceContext
    private EntityManager em;

    @Override
    public User findByUsername(String userName) {
        TypedQuery<User> query = em.createQuery(
                "SELECT NEW test.model.User(u.userName, u.firstName) FROM User u " +
                        "WHERE u.userName = :userName", User.class);
        query.setParameter("userName", userName);
        try {
            return query.getSingleResult();
        } catch (NoResultException e){
            return null;
        }
    }

    @Override
    public List<User> getAll() {
        TypedQuery<User> query = em.createQuery(
                "SELECT NEW test.model.User(u.userName, u.firstName) from User u",
                User.class);
        
        return query.getResultList();
    }
}
