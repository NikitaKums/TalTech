package test.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.PropertySource;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.authentication.logout.LogoutFilter;
import test.config.security.handlers.ApiAccessDeniedHandler;
import test.config.security.handlers.ApiEntryPoint;
import test.config.security.handlers.ApiLogoutSuccessHandler;
import test.config.security.jwt.JwtAuthenticationFilter;
import test.config.security.jwt.JwtAuthorizationFilter;

@Configuration
@EnableWebSecurity
@PropertySource("classpath:/application.properties")
public class SecurityConfig extends WebSecurityConfigurerAdapter {

    @Value("${jwt.signing.key}")
    private String jwtKey;

    @Override
    protected void configure(HttpSecurity http) throws Exception {
        http.csrf().disable();

        http.authorizeRequests()
                .antMatchers("/api/version").permitAll()
                .antMatchers("/api/login").permitAll()
                .antMatchers("/api/logout").permitAll()
                .antMatchers("/api/users").hasRole("ADMIN")
                .antMatchers("/api/admin/**").hasRole("ADMIN")
                .antMatchers("/api/**").authenticated();

        http.exceptionHandling()
                .authenticationEntryPoint(new ApiEntryPoint());

        http.exceptionHandling()
                .accessDeniedHandler(new ApiAccessDeniedHandler());

        var loginFilter = new JwtAuthenticationFilter(authenticationManager(), "/api/login", jwtKey);
        http.addFilterAfter(loginFilter, LogoutFilter.class);

        var jwtAuthFilter = new JwtAuthorizationFilter(authenticationManager(), jwtKey);
        http.addFilterBefore(jwtAuthFilter, LogoutFilter.class);

        http.sessionManagement()
                .sessionCreationPolicy(SessionCreationPolicy.STATELESS);

        http.logout().logoutUrl("/api/logout")
                .logoutSuccessHandler(new ApiLogoutSuccessHandler());

    }

    @Override
    protected void configure(AuthenticationManagerBuilder builder) throws Exception {
        builder.inMemoryAuthentication()
                .passwordEncoder(new BCryptPasswordEncoder())
                .withUser("user")
                .password("$2a$10$12SRAEWFUh2GA91Vjef0iOTfh7lGQqE2FfBhTd0a5rHyuacnlubWS")
                .roles("USER")
                .and()
                .withUser("admin")
                .password("$2a$10$eDKysZLA3RNedOgeB0vQjOh0TbvcXNtcISkFVjBEPS9LDExMQ1phW")
                .roles("USER", "ADMIN");
    }
}
