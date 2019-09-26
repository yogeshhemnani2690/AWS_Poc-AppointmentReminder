import { Component, OnInit, ViewChild } from '@angular/core';
import { AdminService } from '../admin.service';
import { MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  displayedColumns: string[] = ['pocAppointmentId', 'pocAppointmentDate', 'pocEmail', 'pocPatientName','pocDoctorName'];

  Appointments:{
    pocAppointmentId:number,
    pocAppointmentDate:string,
    pocEmail:string,
    pocPatientName:string,
    pocDoctorName:string
  }[]=[];


  dataSource;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private _adminService:AdminService) { }
  ngOnInit() {
    this.GetAllAppointments();
  }

  GetAllAppointments(){
    this._adminService.GetAllAppointments().subscribe((data:Object)=>{
      (<any>data).items.forEach(element => {
        this.Appointments.push(element)      
      });  
      this.dataSource = new MatTableDataSource(this.Appointments);
      this.dataSource.sort = this.sort;
    });
    }
}
