package test.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Column;
import javax.persistence.Embeddable;
import javax.validation.constraints.Min;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Embeddable
public class OrderRow {

    @NotNull
    @Min(1)
    @Column(name = "price")
    private int price;

    @NotNull
    @Min(1)
    @Column(name = "quantity")
    private int quantity;

    @NotNull
    @Size(min = 3, max = 500)
    @Column(name = "item_name")
    private String itemName;
}
