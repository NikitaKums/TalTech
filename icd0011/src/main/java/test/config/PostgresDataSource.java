package test.config;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.PropertySource;
import org.springframework.core.env.Environment;
import org.springframework.jdbc.datasource.DriverManagerDataSource;

import javax.sql.DataSource;

@Configuration
@PropertySource("classpath:/application.properties")
public class PostgresDataSource {

    @Autowired
    private Environment environment;

    @Bean
    public DataSource dataSource() {
        DriverManagerDataSource ds = new DriverManagerDataSource();
        ds.setDriverClassName("org.postgresql.Driver");
        ds.setUsername(environment.getProperty("postgres.user"));
        ds.setPassword(environment.getProperty("postgres.pass"));
        ds.setUrl(environment.getProperty("postgres.url"));
        return ds;
    }

    @Bean("dialect")
    public String dialect() {
        return "org.hibernate.dialect.PostgreSQL10Dialect";
    }
}
