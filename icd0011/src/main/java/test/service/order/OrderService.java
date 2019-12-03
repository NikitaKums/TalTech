package test.service.order;

import test.model.Installment;
import test.model.Order;
import test.service.BaseService;

import java.time.LocalDate;
import java.util.List;

public interface OrderService extends BaseService<Order> {
    List<Installment> getInstallments(long id, LocalDate start, LocalDate end);
}
