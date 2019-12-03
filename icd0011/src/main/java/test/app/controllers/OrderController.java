package test.app.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.*;
import test.model.Installment;
import test.model.Order;
import test.service.order.OrderService;

import javax.validation.Valid;
import java.time.LocalDate;
import java.util.List;

@RestController
public class OrderController {

    @Autowired
    private OrderService orderService;

    @GetMapping("orders")
    public List<Order> getOrders() {
        return orderService.getAll();
    }

    @GetMapping("orders/{id}")
    public Order getOrder(@PathVariable Long id) {
        return orderService.findById(id);
    }

    @GetMapping("orders/{id}/installments")
    public List<Installment> getInstallments(@PathVariable Long id,
                                                          @RequestParam
                                                          @DateTimeFormat(pattern = "yyyy-MM-dd") LocalDate start,
                                                          @RequestParam
                                                          @DateTimeFormat(pattern = "yyyy-MM-dd") LocalDate end) {

        return orderService.getInstallments(id, start, end);
    }

    @PostMapping("orders")
    public Order saveOrder(@RequestBody @Valid Order order) {
        return orderService.save(order);
    }

    @DeleteMapping("orders/{id}")
    public void deleteOrder(@PathVariable Long id) {
        orderService.deleteById(id);
    }
}
