import { Component, OnInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { SaleService } from '../../services/sale-service/sale.service';
import { PurchaseService } from '../../services/purchase-service/purchase.service';
import { DatePipe } from '@angular/common';
import { CHART_COLORS, transparentize } from '../charts/chart-utils';  // Importing the utils

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css'],
  providers: [DatePipe]
})
export class ChartsComponent implements OnInit {
  salesData: any[] = [];
  purchasesData: any[] = [];
  isLoading: boolean = true;

  constructor(
    private saleService: SaleService,
    private purchaseService: PurchaseService,
    private datePipe: DatePipe
  ) {
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.saleService.getSales().subscribe(sales => {
      this.salesData = sales;
      this.purchaseService.getPurchases().subscribe(purchases => {
        this.purchasesData = purchases;
        this.createCombinedChart();
        this.createProductChart();
        this.isLoading = false;
      });
    });
  }

  mergeAndAggregateData(salesData: any[], purchasesData: any[]): any[] {
    const aggregatedData: { [key: string]: { sales: number, purchases: number } } = {};

    salesData.forEach(item => {
      const date = this.datePipe.transform(item.saleDate, 'dd-MM-yyyy') || '';
      if (!aggregatedData[date]) {
        aggregatedData[date] = { sales: 0, purchases: 0 };
      }
      aggregatedData[date].sales += item.totalPrice;
    });

    purchasesData.forEach(item => {
      const date = this.datePipe.transform(item.purchaseDate, 'dd-MM-yyyy') || '';
      if (!aggregatedData[date]) {
        aggregatedData[date] = { sales: 0, purchases: 0 };
      }
      aggregatedData[date].purchases += item.totalCost;
    });

    const mergedData = Object.keys(aggregatedData).map(date => ({
      date: date,
      sales: aggregatedData[date].sales,
      purchases: aggregatedData[date].purchases
    }));

    // Sort merged data by date
    return mergedData.sort((a, b) => {
      const dateA = this.parseDate(a.date);
      const dateB = this.parseDate(b.date);
      return dateA.getTime() - dateB.getTime();
    });
  }

  mergeAndAggregateDataByProduct(salesData: any[], purchasesData: any[]): any[] {
    const aggregatedData: { [key: string]: { sales: number, purchases: number } } = {};

    salesData.forEach(item => {
      const product = item.productName || 'Unknown Product';
      if (!aggregatedData[product]) {
        aggregatedData[product] = { sales: 0, purchases: 0 };
      }
      aggregatedData[product].sales += item.quantitySold;
    });

    purchasesData.forEach(item => {
      const product = item.productName || 'Unknown Product';
      if (!aggregatedData[product]) {
        aggregatedData[product] = { sales: 0, purchases: 0 };
      }
      aggregatedData[product].purchases += item.quantityPurchased;
    });

    return Object.keys(aggregatedData).map(product => ({
      productName: product,
      sales: aggregatedData[product].sales,
      purchases: aggregatedData[product].purchases
    }));
  }

  parseDate(dateString: string): Date {
    const [day, month, year] = dateString.split('-').map(Number);
    return new Date(year, month - 1, day);
  }

  createCombinedChart(): void {
    const mergedData = this.mergeAndAggregateData(this.salesData, this.purchasesData);
    const formattedDates = mergedData.map(data => data.date);
    const salesData = mergedData.map(data => data.sales);
    const purchasesData = mergedData.map(data => data.purchases);

    new Chart('combinedChart', {
      type: 'bar',
      data: {
        labels: formattedDates,
        datasets: [
          {
            label: 'Sales',
            data: salesData,
            borderColor: CHART_COLORS.blue,
            backgroundColor: transparentize(CHART_COLORS.blue, 0.5),
            borderWidth: 2,
            borderRadius: 5,
            borderSkipped: false,
          },
          {
            label: 'Purchases',
            data: purchasesData,
            borderColor: CHART_COLORS.red,
            backgroundColor: transparentize(CHART_COLORS.red, 0.5),
            borderWidth: 2,
            borderRadius: 5,
            borderSkipped: false,
          }
        ]
      },
      options: {
        maintainAspectRatio: false,
        scales: {
          x: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Dates'
            }
          },
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Amount'
            }
          }
        }
      }
    });
  }

  createProductChart(): void {
    const mergedDataByProduct = this.mergeAndAggregateDataByProduct(this.salesData, this.purchasesData);
    const productNames = mergedDataByProduct.map(data => data.productName);
    const salesQuantities = mergedDataByProduct.map(data => data.sales);
    const purchaseQuantities = mergedDataByProduct.map(data => data.purchases);

    new Chart('productChart', {
      type: 'bar',
      data: {
        labels: productNames,
        datasets: [
          {
            label: 'Sales Quantity',
            data: salesQuantities,
            borderColor: CHART_COLORS.blue,
            backgroundColor: transparentize(CHART_COLORS.blue, 0.5),
            borderWidth: 2,
            borderRadius: 5,
            borderSkipped: false,
          },
          {
            label: 'Purchases Quantity',
            data: purchaseQuantities,
            borderColor: CHART_COLORS.red,
            backgroundColor: transparentize(CHART_COLORS.red, 0.5),
            borderWidth: 2,
            borderRadius: 5,
            borderSkipped: false,
          }
        ]
      },
      options: {
        maintainAspectRatio: false,
        scales: {
          x: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Product Names'
            }
          },
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Quantity'
            }
          }
        }
      }
    });
  }
}
