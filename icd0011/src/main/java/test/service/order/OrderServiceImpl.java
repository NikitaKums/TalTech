package test.service.order;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import test.dao.order.OrderDao;
import test.model.Installment;
import test.model.Order;
import test.model.OrderRow;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

@Service
public class OrderServiceImpl implements OrderService {

    @Autowired
    private OrderDao orderDao;

    @Override
    public Order findById(long orderId) {
        return orderDao.findById(orderId);
    }

    @Override
    public Order save(Order order) {
        return orderDao.save(order);
    }

    @Override
    public List<Order> getAll() {
        return orderDao.getAll();
    }

    @Override
    public void deleteById(long id) {
        orderDao.deleteById(id);
    }

    @Override
    public List<Installment> getInstallments(long id, LocalDate start, LocalDate end){
        List<Installment> result = new ArrayList<>();
        Order order = orderDao.findById(id);
        List<LocalDate> months = getMonthsBetweenDates(start, end);
        int orderValue = getOrderRowTotalValue(order.getOrderRows());

        if (orderValue > 3) {
            int modulus = orderValue % months.size();
            int installmentBaseSize = Math.floorDiv(orderValue, months.size());

            while (installmentBaseSize < 3) {
                months.remove(months.size() - 1);
                installmentBaseSize = Math.floorDiv(orderValue, months.size());
            }

            Installment installment;

            for (LocalDate month : months) {
                installment = new Installment();
                installment.setAmount(installmentBaseSize);
                installment.setDate(month.toString());
                result.add(installment);

                orderValue -= installmentBaseSize;
            }

            while (modulus > 0){
                for (int i = result.size() - 1; i >= 0; i--) {
                    if (modulus <= 0){
                        break;
                    }
                    Installment tempInstallmentRow = result.get(i);
                    tempInstallmentRow.setAmount(tempInstallmentRow.getAmount() + 1);
                    result.set(i, tempInstallmentRow);
                    modulus -= 1;
                }
            }
        }
        return result;
    }

    private List<LocalDate> getMonthsBetweenDates(LocalDate start, LocalDate end) {
        List<LocalDate> result = new ArrayList<>();
        while (start.isBefore(end) || start.equals(end)) {
            result.add(start);
            start = start.withDayOfMonth(1).plusMonths(1);
        }
        return result;
    }

    private int getOrderRowTotalValue(List<OrderRow> orderRows) {
        int result = 0;
        for (OrderRow orderRow : orderRows) {
            result += orderRow.getPrice() * orderRow.getQuantity();
        }
        return result;
    }
}
