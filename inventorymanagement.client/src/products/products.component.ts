import { Component, OnInit, ViewChild, ElementRef, TemplateRef} from '@angular/core';
import { Product } from '../Models/product.model';
import { ProductService } from '../services/product-service/product.service';
import { SaleService } from '../services/sale-service/sale.service';
import { PurchaseService } from '../services/purchase-service/purchase.service';
import { NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  @ViewChild('ngbToast', { static: true }) ngbToast!: ElementRef;
  @ViewChild('saleModal') saleModal!: TemplateRef<any>;
  @ViewChild('purchaseModal') purchaseModal!: TemplateRef<any>;
  @ViewChild('updateModal') updateModal!: TemplateRef<any>;
  public products: Product[] = [];
  public selectedProduct: Product = { name: '', description: '', quantityInStock: 0, productID: 0 };
  public newProduct: Product = { name: '', description: '', quantityInStock: 0, productID: 0 };
  public isEditing: boolean = false;
  toastMessage: string = '';
  showToastFlag: boolean = false;
  toastType: string = '';
  public saleData = { quantitySold: 0, totalPrice: 0 };
  public showSaleModal: boolean = false;
  public purchaseData = { quantityPurchased: 0, totalCost: 0 };
  public showPurchaseModal: boolean = false;
  public isLoading: boolean = false;

  constructor(private productService: ProductService, private saleService: SaleService, private purchaseService: PurchaseService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.isLoading = true;
    this.productService.getProducts().subscribe(
      (data: Product[]) => {
        this.products = data;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  saveProduct() {
      this.productService.createProduct(this.newProduct).subscribe(
        (newProduct: Product) => {
          this.products.push(newProduct);
          this.resetForm();
          this.showToast('Product Added successfully!', 'success');
        },
        (error) => {
          console.error('Error creating product', error);
          this.showToast('Error creating product. Please try again.', 'error');
        }
      );
    //}
  }
  openUpdateModal(product: Product) {
    this.selectedProduct = { ...product };
    this.modalService.open(this.updateModal);
  }

  updateProduct(modal: any) {
    this.productService.updateProduct(this.selectedProduct.productID, this.selectedProduct).subscribe(
      response => {
        this.showToast('Product updated successfully', 'success');
        modal.close();
        this.getProducts();
      },
      error => {
        this.showToast('Error updating product', 'error');
      }
    );
  }
  deleteProduct(productId: number) {
    this.productService.deleteProduct(productId).subscribe(
      () => {
        this.products = this.products.filter(p => p.productID !== productId);
        this.showToast('Product deleted successfully!', 'success');
      },
      (error) => {
        console.error('Error deleting product', error);
        this.showToast('Error deleting product. Please try again.', 'error');
      }
    );
  }

  resetForm() {
    this.selectedProduct = { name: '', description: '', quantityInStock: 0, productID: 0 };
    this.newProduct = { name: '', description: '', quantityInStock: 0, productID: 0 };
    this.isEditing = false;
  }
  showToast(message: string, type: string) {
    this.toastMessage = message;
    this.toastType = type;
    this.showToastFlag = true;
    setTimeout(() => this.showToastFlag = false, 3000);
  }

  openSaleModal(product: Product) {
    this.selectedProduct = product;
    this.saleData = { quantitySold: 0, totalPrice: 0 };
    this.modalService.open(this.saleModal);
  }
  openPurchaseModal(product: Product) {
    this.selectedProduct = product;
    this.purchaseData = { quantityPurchased: 0, totalCost: 0 };
    this.modalService.open(this.purchaseModal);
  }
  createSale(modal: any) {
    const sale = {
      productId: this.selectedProduct.productID,
      quantitySold: this.saleData.quantitySold,
      totalPrice: this.saleData.totalPrice
    };

    if(sale.quantitySold > this.selectedProduct.quantityInStock)
    {
      modal.close();
      this.showToast('The quantity of the product in stock is less than the sale quantity. Please adjust the sale quantity or restock the product.', 'error');
      return;
    }
    this.saleService.createSale(sale).subscribe(
      response => {
        console.log('Sale created successfully', response);
        this.showToast('Sales recorded successfully!', 'success');
        modal.close();
        this.getProducts();
      },
      error => {
        console.error('Error creating sale', error);
        this.showToast('Error creating sale', 'error');
      }
    );
  }
  createPurchase(modal: any) {
    const purchase = {
      productId: this.selectedProduct.productID,
      quantityPurchased: this.purchaseData.quantityPurchased,
      totalCost: this.purchaseData.totalCost
    };

    this.purchaseService.createPurchase(purchase).subscribe(
      response => {
        console.log('Purchase created successfully', response);
        this.showToast('Purchase recorded successfully!', 'success');
        modal.close();
        this.getProducts();
      },
      error => {
        console.error('Error creating purchase', error);
        this.showToast('Error creating purchase', 'error');
      }
    );
  }
}
