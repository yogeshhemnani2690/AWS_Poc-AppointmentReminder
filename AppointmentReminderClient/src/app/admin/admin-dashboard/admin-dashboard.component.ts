import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}
const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  //displayedColumns: string[] = ['AppointmentId', 'AppointmentDate', 'Email', 'PatientName','DoctorName'];

  Appointments:{
    AppointmentId:number,
    AppointmentDate:string,
    Email:string,
    PatientName:string,
    DoctorName:string
  }[]=[];


  dataSource = ELEMENT_DATA;
  constructor(private _adminService:AdminService) { }
  ngOnInit() {
    this.GetAllAppointments();
  }
//Error in reading ForEach Loop
//Cannot read property 'forEach' of undefined
  GetAllAppointments(){
    this._adminService.GetAllAppointments().subscribe((data:Object)=>{
      (<any>data).Appointments.forEach(element => {
        this.Appointments.push(element)
      });  
    });
    }
}
