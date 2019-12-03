package test.model;

import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.NonNull;
import lombok.RequiredArgsConstructor;

import javax.persistence.*;
import javax.validation.Valid;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;
import java.util.ArrayList;
import java.util.List;

@Data
@Entity
@NoArgsConstructor
@RequiredArgsConstructor
@Table(name = "orders")
public class Order {

    @Id
    @SequenceGenerator(
            name = "my_seq",
            sequenceName = "seq1",
            allocationSize = 1)
    @GeneratedValue(
            strategy = GenerationType.SEQUENCE,
            generator = "my_seq")
    private Long id;

    @NonNull
    @NotNull
    @Column(name = "order_number")
    @Size(min = 2, max = 500)
    private String orderNumber;

    @Valid
    @ElementCollection(fetch = FetchType.LAZY)
    @CollectionTable(
            name = "order_rows",
            joinColumns = @JoinColumn(name = "orders_id", referencedColumnName = "id"))
    private List<OrderRow> orderRows = new ArrayList<>();

}