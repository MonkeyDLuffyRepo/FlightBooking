import { Component, OnInit } from '@angular/core';
import { FlightService } from '../services/flight.service';
import { FlightDetail } from '../models/models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  
  flightList : FlightDetail[];
  constructor(   private router: Router,
     private flightService:FlightService){}
  ngOnInit(): void {
   this.flightService.getAllFlights().subscribe(
       (data: FlightDetail[]) => {
           this.flightList = data;
           console.log('data',data);
       },
     (error:any)=> console.log(error)
   )
  }

  editFlight(flightId){
     this.router.navigate['/flight-from/'+flightId];
  }
}
