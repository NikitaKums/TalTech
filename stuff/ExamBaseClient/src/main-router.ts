import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {RouterConfiguration, Router} from "aurelia-router";
import {AppConfig} from "./app-config";

export var log = LogManager.getLogger('MainRouter');

@autoinject
export class MainRouter {
  
  public router: Router;

  constructor(private appConfig: AppConfig) {
    log.debug('constructor');
  }

  configureRouter(config: RouterConfiguration, router: Router):void {
    log.debug('configureRouter');

    this.router = router;
    config.title = "Exam App - Aurelia";
    config.map(
      [
        {route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home'), nav: false, title: 'Home'},

        {route: 'identity/login', name: 'identity' + 'Login', moduleId: PLATFORM.moduleName('identity/login'), nav: false, title: 'Login'},
        {route: 'identity/register', name: 'identity' + 'Register', moduleId: PLATFORM.moduleName('identity/register'), nav: false, title: 'Register'},
        {route: 'identity/logout', name: 'identity' + 'Logout', moduleId: PLATFORM.moduleName('identity/logout'), nav: false, title: 'Logout'},

        {route: ['deliverys','deliverys/index'], name: 'deliverys' + 'Index', moduleId: PLATFORM.moduleName('deliverys/index'), nav: true, title: 'Deliveries'},
        {route: 'deliverys/create', name: 'deliverys' + 'Create', moduleId: PLATFORM.moduleName('deliverys/create'), nav: false, title: 'Deliveries Create'},
        {route: 'deliverys/edit/:id', name: 'deliverys' + 'Edit', moduleId: PLATFORM.moduleName('deliverys/edit'), nav: false, title: 'Deliveries Edit'},
        {route: 'deliverys/delete/:id', name: 'deliverys' + 'Delete', moduleId: PLATFORM.moduleName('deliverys/delete'), nav: false, title: 'Deliveries Delete'},
        {route: 'deliverys/details/:id', name: 'deliverys' + 'Details', moduleId: PLATFORM.moduleName('deliverys/details'), nav: false, title: 'Deliveries Details'},

        {route: ['drinks','drinks/index'], name: 'drinks' + 'Index', moduleId: PLATFORM.moduleName('drinks/index'), nav: true, title: 'Drinks'},
        {route: 'drinks/create', name: 'drinks' + 'Create', moduleId: PLATFORM.moduleName('drinks/create'), nav: false, title: 'Drinks Create'},
        {route: 'drinks/edit/:id', name: 'drinks' + 'Edit', moduleId: PLATFORM.moduleName('drinks/edit'), nav: false, title: 'Drinks Edit'},
        {route: 'drinks/delete/:id', name: 'drinks' + 'Delete', moduleId: PLATFORM.moduleName('drinks/delete'), nav: false, title: 'Drinks Delete'},
        {route: 'drinks/details/:id', name: 'drinks' + 'Details', moduleId: PLATFORM.moduleName('drinks/details'), nav: false, title: 'Drinks Details'},

        {route: ['drinksinorder','drinksinorder/index'], name: 'drinksinorder' + 'Index', moduleId: PLATFORM.moduleName('drinksinorder/index'), nav: false, title: 'Drinks in order'},
        {route: 'drinksinorder/create/:id', name: 'drinksinorder' + 'Create', moduleId: PLATFORM.moduleName('drinksinorder/create'), nav: false, title: 'Drink in order Create'},
        {route: 'drinksinorder/edit/:id', name: 'drinksinorder' + 'Edit', moduleId: PLATFORM.moduleName('drinksinorder/edit'), nav: false, title: 'Drink in order Edit'},
        {route: 'drinksinorder/delete/:id', name: 'drinksinorder' + 'Delete', moduleId: PLATFORM.moduleName('drinksinorder/delete'), nav: false, title: 'Drink in order Delete'},
        {route: 'drinksinorder/details/:id', name: 'drinksinorder' + 'Details', moduleId: PLATFORM.moduleName('drinksinorder/details'), nav: false, title: 'Drink in order Details'},

        {route: ['orders','orders/index'], name: 'orders' + 'Index', moduleId: PLATFORM.moduleName('orders/index'), nav: true, title: 'Order'},
        {route: 'orders/create', name: 'orders' + 'Create', moduleId: PLATFORM.moduleName('orders/create'), nav: false, title: 'Order Create'},
        {route: 'orders/edit/:id', name: 'orders' + 'Edit', moduleId: PLATFORM.moduleName('orders/edit'), nav: false, title: 'Order Edit'},
        {route: 'orders/delete/:id', name: 'orders' + 'Delete', moduleId: PLATFORM.moduleName('orders/delete'), nav: false, title: 'Order Delete'},
        {route: 'orders/details/:id', name: 'orders' + 'Details', moduleId: PLATFORM.moduleName('orders/details'), nav: false, title: 'Order Details'},

        {route: ['pizzas','pizzas/index'], name: 'pizzas' + 'Index', moduleId: PLATFORM.moduleName('pizzas/index'), nav: true, title: 'Pizza'},
        {route: 'pizzas/create', name: 'pizzas' + 'Create', moduleId: PLATFORM.moduleName('pizzas/create'), nav: false, title: 'Pizza Create'},
        {route: 'pizzas/edit/:id', name: 'pizzas' + 'Edit', moduleId: PLATFORM.moduleName('pizzas/edit'), nav: false, title: 'Pizza Edit'},
        {route: 'pizzas/delete/:id', name: 'pizzas' + 'Delete', moduleId: PLATFORM.moduleName('pizzas/delete'), nav: false, title: 'Pizza Delete'},
        {route: 'pizzas/details/:id', name: 'pizzas' + 'Details', moduleId: PLATFORM.moduleName('pizzas/details'), nav: false, title: 'Pizza Details'},

        {route: ['pizzasinorder','pizzasinorder/index'], name: 'pizzasinorder' + 'Index', moduleId: PLATFORM.moduleName('pizzasinorder/index'), nav: false, title: 'Pizza in order'},
        {route: 'pizzasinorder/create/:id', name: 'pizzasinorder' + 'Create', moduleId: PLATFORM.moduleName('pizzasinorder/create'), nav: false, title: 'Pizza in order Create'},
        {route: 'pizzasinorder/edit/:id', name: 'pizzasinorder' + 'Edit', moduleId: PLATFORM.moduleName('pizzasinorder/edit'), nav: false, title: 'Pizza in order Edit'},
        {route: 'pizzasinorder/delete/:id', name: 'pizzasinorder' + 'Delete', moduleId: PLATFORM.moduleName('pizzasinorder/delete'), nav: false, title: 'Pizza in order Delete'},
        {route: 'pizzasinorder/details/:id', name: 'pizzasinorder' + 'Details', moduleId: PLATFORM.moduleName('pizzasinorder/details'), nav: false, title: 'Pizza in order Details'},

        {route: ['toppings','toppings/index'], name: 'toppings' + 'Index', moduleId: PLATFORM.moduleName('toppings/index'), nav: true, title: 'Toppings'},
        {route: 'toppings/create', name: 'toppings' + 'Create', moduleId: PLATFORM.moduleName('toppings/create'), nav: false, title: 'Toppings Create'},
        {route: 'toppings/edit/:id', name: 'toppings' + 'Edit', moduleId: PLATFORM.moduleName('toppings/edit'), nav: false, title: 'Toppings Edit'},
        {route: 'toppings/delete/:id', name: 'toppings' + 'Delete', moduleId: PLATFORM.moduleName('toppings/delete'), nav: false, title: 'Toppings Delete'},
        {route: 'toppings/details/:id', name: 'toppings' + 'Details', moduleId: PLATFORM.moduleName('toppings/details'), nav: false, title: 'Toppings Details'},

        {route: ['toppingsonpizza','toppingsonpizza/index'], name: 'toppingsonpizza' + 'Index', moduleId: PLATFORM.moduleName('toppingsonpizza/index'), nav: false, title: 'Topping on pizza'},
        {route: 'toppingsonpizza/create/:id', name: 'toppingsonpizza' + 'Create', moduleId: PLATFORM.moduleName('toppingsonpizza/create'), nav: false, title: 'Topping on pizza Create'},
        {route: 'toppingsonpizza/edit/:id', name: 'toppingsonpizza' + 'Edit', moduleId: PLATFORM.moduleName('toppingsonpizza/edit'), nav: false, title: 'Topping on pizza Edit'},
        {route: 'toppingsonpizza/delete/:id', name: 'toppingsonpizza' + 'Delete', moduleId: PLATFORM.moduleName('toppingsonpizza/delete'), nav: false, title: 'Topping on pizza Delete'},
        {route: 'toppingsonpizza/details/:id', name: 'toppingsonpizza' + 'Details', moduleId: PLATFORM.moduleName('toppingsonpizza/details'), nav: false, title: 'Topping on pizza Details'},


        /*{route: ['categories','categories/index'], name: 'categories' + 'Index', moduleId: PLATFORM.moduleName('categories/index'), nav: true, title: 'Categories'},
        {route: 'categories/create', name: 'categories' + 'Create', moduleId: PLATFORM.moduleName('categories/create'), nav: false, title: 'Categories Create'},
        {route: 'categories/edit/:id', name: 'categories' + 'Edit', moduleId: PLATFORM.moduleName('categories/edit'), nav: false, title: 'Categories Edit'},
        {route: 'categories/delete/:id', name: 'categories' + 'Delete', moduleId: PLATFORM.moduleName('categories/delete'), nav: false, title: 'Categories Delete'},
        {route: 'categories/details/:id', name: 'categories' + 'Details', moduleId: PLATFORM.moduleName('categories/details'), nav: false, title: 'Categories Details'},
*/
        {route: ['comments','comments/index'], name: 'comments' + 'Index', moduleId: PLATFORM.moduleName('comments/index'), nav: false, title: 'Comments'},
        {route: 'comments/create', name: 'comments' + 'Create', moduleId: PLATFORM.moduleName('comments/create'), nav: false, title: 'Comments Create'},
        {route: 'comments/edit/:id', name: 'comments' + 'Edit', moduleId: PLATFORM.moduleName('comments/edit'), nav: false, title: 'Comments Edit'},
        {route: 'comments/delete/:id', name: 'comments' + 'Delete', moduleId: PLATFORM.moduleName('comments/delete'), nav: false, title: 'Comments Delete'},
        {route: 'comments/details/:id', name: 'comments' + 'Details', moduleId: PLATFORM.moduleName('comments/details'), nav: false, title: 'Comments Details'},
      ]
    );

  }

}
