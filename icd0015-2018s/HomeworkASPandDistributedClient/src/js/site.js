let $ = require('jquery');

export let sellPriceValue;
export function CalcSellPrice(){
  let buyPriceField;
  let percentageField;
  let sellPriceField;
  $(() => {
    buyPriceField = document.getElementById("Product_BuyPrice");
    percentageField = document.getElementById("Product_PercentageAddedToBuyPrice");
    sellPriceField = document.getElementById("Product_SellPrice");
    buyPriceField.oninput = function() {CalcStuff()};
    percentageField.oninput = function() {CalcStuff()};
  });
  function CalcStuff(){
    let buyPrice = parseFloat(buyPriceField.value.replace(",","."));
    sellPriceField.value = String(Number((buyPrice + (buyPrice * percentageField.value / 100))).toFixed(2));
    sellPriceValue = sellPriceField.value
  }
}
