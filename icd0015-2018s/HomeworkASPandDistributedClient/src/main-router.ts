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
    config.title = "ShopApp - Aurelia";
    config.map(
      [
        {route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home'), nav: false, title: 'Home'},

        {route: 'identity/login', name: 'identity' + 'Login', moduleId: PLATFORM.moduleName('identity/login'), nav: false, title: 'Login'},
        {route: 'identity/register', name: 'identity' + 'Register', moduleId: PLATFORM.moduleName('identity/register'), nav: false, title: 'Register'},
        {route: 'identity/logout', name: 'identity' + 'Logout', moduleId: PLATFORM.moduleName('identity/logout'), nav: false, title: 'Logout'},

        /*{route: ['users','users/index'], name: 'users' + 'Index', moduleId: PLATFORM.moduleName('users/index'), nav: true, title: 'Users'},
        {route: 'users/create', name: 'users' + 'Create', moduleId: PLATFORM.moduleName('users/create'), nav: false, title: 'Users Create'},
        {route: 'users/edit/:id', name: 'users' + 'Edit', moduleId: PLATFORM.moduleName('users/edit'), nav: false, title: 'Users Edit'},
        {route: 'users/delete/:id', name: 'users' + 'Delete', moduleId: PLATFORM.moduleName('users/delete'), nav: false, title: 'Users Delete'},
        {route: 'users/details/:id', name: 'users' + 'Details', moduleId: PLATFORM.moduleName('users/details'), nav: false, title: 'Users Details'},*/

        {route: ['categories','categories/index'], name: 'categories' + 'Index', moduleId: PLATFORM.moduleName('categories/index'), nav: true, title: 'Categories'},
        {route: 'categories/create', name: 'categories' + 'Create', moduleId: PLATFORM.moduleName('categories/create'), nav: false, title: 'Categories Create'},
        {route: 'categories/edit/:id', name: 'categories' + 'Edit', moduleId: PLATFORM.moduleName('categories/edit'), nav: false, title: 'Categories Edit'},
        {route: 'categories/delete/:id', name: 'categories' + 'Delete', moduleId: PLATFORM.moduleName('categories/delete'), nav: false, title: 'Categories Delete'},
        {route: 'categories/details/:id', name: 'categories' + 'Details', moduleId: PLATFORM.moduleName('categories/details'), nav: false, title: 'Categories Details'},

        {route: ['comments','comments/index'], name: 'comments' + 'Index', moduleId: PLATFORM.moduleName('comments/index'), nav: true, title: 'Comments'},
        {route: 'comments/create', name: 'comments' + 'Create', moduleId: PLATFORM.moduleName('comments/create'), nav: false, title: 'Comments Create'},
        {route: 'comments/edit/:id', name: 'comments' + 'Edit', moduleId: PLATFORM.moduleName('comments/edit'), nav: false, title: 'Comments Edit'},
        {route: 'comments/delete/:id', name: 'comments' + 'Delete', moduleId: PLATFORM.moduleName('comments/delete'), nav: false, title: 'Comments Delete'},
        {route: 'comments/details/:id', name: 'comments' + 'Details', moduleId: PLATFORM.moduleName('comments/details'), nav: false, title: 'Comments Details'},

        {route: ['defects','defects/index'], name: 'defects' + 'Index', moduleId: PLATFORM.moduleName('defects/index'), nav: true, title: 'Defects'},
        {route: 'defects/create', name: 'defects' + 'Create', moduleId: PLATFORM.moduleName('defects/create'), nav: false, title: 'Defects Create'},
        {route: 'defects/edit/:id', name: 'defects' + 'Edit', moduleId: PLATFORM.moduleName('defects/edit'), nav: false, title: 'Defects Edit'},
        {route: 'defects/delete/:id', name: 'defects' + 'Delete', moduleId: PLATFORM.moduleName('defects/delete'), nav: false, title: 'Defects Delete'},
        {route: 'defects/details/:id', name: 'defects' + 'Details', moduleId: PLATFORM.moduleName('defects/details'), nav: false, title: 'Defects Details'},
        
        {route: ['inventorys','inventorys/index'], name: 'inventorys' + 'Index', moduleId: PLATFORM.moduleName('inventorys/index'), nav: true, title: 'Inventory'},
        {route: 'inventorys/create', name: 'inventorys' + 'Create', moduleId: PLATFORM.moduleName('inventorys/create'), nav: false, title: 'Inventory Create'},
        {route: 'inventorys/edit/:id', name: 'inventorys' + 'Edit', moduleId: PLATFORM.moduleName('inventorys/edit'), nav: false, title: 'Inventory Edit'},
        {route: 'inventorys/delete/:id', name: 'inventorys' + 'Delete', moduleId: PLATFORM.moduleName('inventorys/delete'), nav: false, title: 'Inventory Delete'},
        {route: 'inventorys/details/:id', name: 'inventorys' + 'Details', moduleId: PLATFORM.moduleName('inventorys/details'), nav: false, title: 'Inventory Details'},

        {route: ['manufacturers','manufacturers/index'], name: 'manufacturers' + 'Index', moduleId: PLATFORM.moduleName('manufacturers/index'), nav: true, title: 'ManuFacturers'},
        {route: 'manufacturers/create', name: 'manufacturers' + 'Create', moduleId: PLATFORM.moduleName('manufacturers/create'), nav: false, title: 'ManuFacturers Create'},
        {route: 'manufacturers/edit/:id', name: 'manufacturers' + 'Edit', moduleId: PLATFORM.moduleName('manufacturers/edit'), nav: false, title: 'ManuFacturers Edit'},
        {route: 'manufacturers/delete/:id', name: 'manufacturers' + 'Delete', moduleId: PLATFORM.moduleName('manufacturers/delete'), nav: false, title: 'ManuFacturers Delete'},
        {route: 'manufacturers/details/:id', name: 'manufacturers' + 'Details', moduleId: PLATFORM.moduleName('manufacturers/details'), nav: false, title: 'ManuFacturers Details'},

        {route: ['orders','orders/index'], name: 'orders' + 'Index', moduleId: PLATFORM.moduleName('orders/index'), nav: true, title: 'Orders'},
        {route: 'orders/create', name: 'orders' + 'Create', moduleId: PLATFORM.moduleName('orders/create'), nav: false, title: 'Orders Create'},
        {route: 'orders/edit/:id', name: 'orders' + 'Edit', moduleId: PLATFORM.moduleName('orders/edit'), nav: false, title: 'Orders Edit'},
        {route: 'orders/delete/:id', name: 'orders' + 'Delete', moduleId: PLATFORM.moduleName('orders/delete'), nav: false, title: 'Orders Delete'},
        {route: 'orders/details/:id', name: 'orders' + 'Details', moduleId: PLATFORM.moduleName('orders/details'), nav: false, title: 'Orders Details'},
        {route: 'orders/received/:id', name: 'orders' + 'Received', moduleId: PLATFORM.moduleName('orders/received'), nav: false, title: 'Orders Received'},
        
        {route: ['products','products/index'], name: 'products' + 'Index', moduleId: PLATFORM.moduleName('products/index'), nav: true, title: 'Products'},
        {route: 'products/create', name: 'products' + 'Create', moduleId: PLATFORM.moduleName('products/create'), nav: false, title: 'Products Create'},
        {route: 'products/edit/:id', name: 'products' + 'Edit', moduleId: PLATFORM.moduleName('products/edit'), nav: false, title: 'Products Edit'},
        {route: 'products/delete/:id', name: 'products' + 'Delete', moduleId: PLATFORM.moduleName('products/delete'), nav: false, title: 'Products Delete'},
        {route: 'products/details/:id', name: 'products' + 'Details', moduleId: PLATFORM.moduleName('products/details'), nav: false, title: 'Products Details'},

        //{route: ['productsincategory','productsincategory/index'], name: 'productsincategory' + 'Index', moduleId: PLATFORM.moduleName('productsincategory/index'), nav: true, title: 'Products In Category'},
        {route: 'productsincategory/create/:id', name: 'productsincategory' + 'Create', moduleId: PLATFORM.moduleName('productsincategory/create'), nav: false, title: 'Products In Category Create'},
        {route: 'productsincategory/edit/:id', name: 'productsincategory' + 'Edit', moduleId: PLATFORM.moduleName('productsincategory/edit'), nav: false, title: 'Products In Category Edit'},
        {route: 'productsincategory/delete/:id', name: 'productsincategory' + 'Delete', moduleId: PLATFORM.moduleName('productsincategory/delete'), nav: false, title: 'Products In Category Delete'},
        //{route: 'productsincategory/details/:id', name: 'productsincategory' + 'Details', moduleId: PLATFORM.moduleName('productsincategory/details'), nav: false, title: 'Products In Category Details'},

        //{route: ['productsinorder','productsinorder/index'], name: 'productsinorder' + 'Index', moduleId: PLATFORM.moduleName('productsinorder/index'), nav: true, title: 'Products In Order'},
        //{route: 'productsinorder/received/:id', name: 'productsinorder' + 'Received', moduleId: PLATFORM.moduleName('productsinorder/received'), nav: false, title: 'Products In Order Received'},
        {route: 'productsinorder/create/:id', name: 'productsinorder' + 'Create', moduleId: PLATFORM.moduleName('productsinorder/create'), nav: false, title: 'Products In Order Create'},
        {route: 'productsinorder/edit/:id', name: 'productsinorder' + 'Edit', moduleId: PLATFORM.moduleName('productsinorder/edit'), nav: false, title: 'Products In Order Edit'},
        {route: 'productsinorder/delete/:id', name: 'productsinorder' + 'Delete', moduleId: PLATFORM.moduleName('productsinorder/delete'), nav: false, title: 'Products In Order Delete'},
        //{route: 'productsinorder/details/:id', name: 'productsinorder' + 'Details', moduleId: PLATFORM.moduleName('productsinorder/details'), nav: false, title: 'Products In Order Details'},

        //{route: ['productsreturned','productsreturned/index'], name: 'productsreturned' + 'Index', moduleId: PLATFORM.moduleName('productsreturned/index'), nav: true, title: 'Products Returned'},
        {route: 'productsreturned/create/:id', name: 'productsreturned' + 'Create', moduleId: PLATFORM.moduleName('productsreturned/create'), nav: false, title: 'Products Returned Create'},
        {route: 'productsreturned/edit/:id', name: 'productsreturned' + 'Edit', moduleId: PLATFORM.moduleName('productsreturned/edit'), nav: false, title: 'Products Returned Edit'},
        {route: 'productsreturned/delete/:id', name: 'productsreturned' + 'Delete', moduleId: PLATFORM.moduleName('productsreturned/delete'), nav: false, title: 'Products Returned Delete'},
        //{route: 'productsreturned/details/:id', name: 'productsreturned' + 'Details', moduleId: PLATFORM.moduleName('productsreturned/details'), nav: false, title: 'Products Returned Details'},

        //{route: ['productssold','productssold/index'], name: 'productssold' + 'Index', moduleId: PLATFORM.moduleName('productssold/index'), nav: true, title: 'Products Sold'},
        {route: 'productssold/create/:id', name: 'productssold' + 'Create', moduleId: PLATFORM.moduleName('productssold/create'), nav: false, title: 'Products Sold Create'},
        {route: 'productssold/edit/:id', name: 'productssold' + 'Edit', moduleId: PLATFORM.moduleName('productssold/edit'), nav: false, title: 'Products Sold Edit'},
        {route: 'productssold/delete/:id', name: 'productssold' + 'Delete', moduleId: PLATFORM.moduleName('productssold/delete'), nav: false, title: 'Products Sold Delete'},
       // {route: 'productssold/details/:id', name: 'productssold' + 'Details', moduleId: PLATFORM.moduleName('productssold/details'), nav: false, title: 'Products Sold Details'},

        //{route: ['productswithdefect','productswithdefect/index'], name: 'productswithdefect' + 'Index', moduleId: PLATFORM.moduleName('productswithdefect/index'), nav: true, title: 'Products With Defect'},
        {route: 'productswithdefect/create/:id', name: 'productswithdefect' + 'Create', moduleId: PLATFORM.moduleName('productswithdefect/create'), nav: false, title: 'Products With Defect Create'},
        {route: 'productswithdefect/edit/:id', name: 'productswithdefect' + 'Edit', moduleId: PLATFORM.moduleName('productswithdefect/edit'), nav: false, title: 'Products With Defect Edit'},
        {route: 'productswithdefect/delete/:id', name: 'productswithdefect' + 'Delete', moduleId: PLATFORM.moduleName('productswithdefect/delete'), nav: false, title: 'Products With Defect Delete'},
        //{route: 'productswithdefect/details/:id', name: 'productswithdefect' + 'Details', moduleId: PLATFORM.moduleName('productswithdefect/details'), nav: false, title: 'Products With Defect Details'},
        
        {route: ['returns','returns/index'], name: 'returns' + 'Index', moduleId: PLATFORM.moduleName('returns/index'), nav: true, title: 'Returns'},
        {route: 'returns/create', name: 'returns' + 'Create', moduleId: PLATFORM.moduleName('returns/create'), nav: false, title: 'Returns Create'},
        {route: 'returns/edit/:id', name: 'returns' + 'Edit', moduleId: PLATFORM.moduleName('returns/edit'), nav: false, title: 'Returns Edit'},
        {route: 'returns/delete/:id', name: 'returns' + 'Delete', moduleId: PLATFORM.moduleName('returns/delete'), nav: false, title: 'Returns Delete'},
        {route: 'returns/details/:id', name: 'returns' + 'Details', moduleId: PLATFORM.moduleName('returns/details'), nav: false, title: 'Returns Details'},

        {route: ['sales','sales/index'], name: 'sales' + 'Index', moduleId: PLATFORM.moduleName('sales/index'), nav: true, title: 'Sales'},
        {route: 'sales/create', name: 'sales' + 'Create', moduleId: PLATFORM.moduleName('sales/create'), nav: false, title: 'Sales Create'},
        {route: 'sales/edit/:id', name: 'sales' + 'Edit', moduleId: PLATFORM.moduleName('sales/edit'), nav: false, title: 'Sales Edit'},
        {route: 'sales/delete/:id', name: 'sales' + 'Delete', moduleId: PLATFORM.moduleName('sales/delete'), nav: false, title: 'Sales Delete'},
        {route: 'sales/details/:id', name: 'sales' + 'Details', moduleId: PLATFORM.moduleName('sales/details'), nav: false, title: 'Sales Details'},

        {route: ['shippers','shippers/index'], name: 'shippers' + 'Index', moduleId: PLATFORM.moduleName('shippers/index'), nav: true, title: 'Shippers'},
        {route: 'shippers/create', name: 'shippers' + 'Create', moduleId: PLATFORM.moduleName('shippers/create'), nav: false, title: 'Shippers Create'},
        {route: 'shippers/edit/:id', name: 'shippers' + 'Edit', moduleId: PLATFORM.moduleName('shippers/edit'), nav: false, title: 'Shippers Edit'},
        {route: 'shippers/delete/:id', name: 'shippers' + 'Delete', moduleId: PLATFORM.moduleName('shippers/delete'), nav: false, title: 'Shippers Delete'},
        {route: 'shippers/details/:id', name: 'shippers' + 'Details', moduleId: PLATFORM.moduleName('shippers/details'), nav: false, title: 'Shippers Details'},

        {route: ['shops','shops/index'], name: 'shops' + 'Index', moduleId: PLATFORM.moduleName('shops/index'), nav: true, title: 'Shops'},
        {route: 'shops/create', name: 'shops' + 'Create', moduleId: PLATFORM.moduleName('shops/create'), nav: false, title: 'Shops Create'},
        {route: 'shops/edit/:id', name: 'shops' + 'Edit', moduleId: PLATFORM.moduleName('shops/edit'), nav: false, title: 'Shops Edit'},
        {route: 'shops/delete/:id', name: 'shops' + 'Delete', moduleId: PLATFORM.moduleName('shops/delete'), nav: false, title: 'Shops Delete'},
        {route: 'shops/details/:id', name: 'shops' + 'Details', moduleId: PLATFORM.moduleName('shops/details'), nav: false, title: 'Shops Details'},
      ]
    );

  }

}
