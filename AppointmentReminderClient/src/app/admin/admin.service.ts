import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  //apiUrl:string="https://localhost:5001/api/DynamoDb/";
  apiUrl:string="http://localhost:64202/api/Loader/"
  constructor(private http:HttpClient) { }
  GetAllAppointments(){
    return this.http.get(this.apiUrl+"GetNextDayAppointmentIds");
  }
}
