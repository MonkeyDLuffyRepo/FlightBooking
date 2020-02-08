import { Component, OnInit } from '@angular/core';
import { AirportBaseModel, PlaneBaseModel, FlightDetail, AirportDetail, PlaneDetail } from '../models/models';
import { FlightService } from '../services/flight.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-flight-form',
  templateUrl: './flight-form.component.html',
  styleUrls: ['./flight-form.component.css']
})
export class FlightFormComponent implements OnInit {

  airports : AirportDetail[] = [];
  planes : PlaneDetail[]=[];
  currentFlight : FlightDetail;
  selectedAirportFrom:number=0;
  selectedAirportTo:number=0;
  // airportFrom :AirportDetail = {id:1,name:'',country:'',city:'',latitude:0,longitude:0}
  // airportTo :AirportDetail = {id:1,name:'',country:'',city:'',latitude:0,longitude:0}
  airportFromInfo : string ='';
  airportToInfo : string ='';
  constructor( private toastr: ToastrService,
    private flightService:FlightService) { }

  ngOnInit() {
   this.flightService.getAllAirports().subscribe(
     (data:AirportDetail[]) =>{
        this.airports = data;
     },
     (error : any)=>{
       console.log(error);
     }
   );
   this.flightService.getAllPlanes().subscribe(
     (data : PlaneDetail[])=>{
       this.planes = data;
     },
     (error :any)=>{
       console.log(error);
     }
   )

  }
  getSelectedAirportFrom(event: Event):void{
    const value = event.target['options'][event.target['options'].selectedIndex].value;
    this.selectedAirportFrom = Number(value);
    
    this.airportFromInfo = this.getCityAndCountry(this.selectedAirportFrom );
  }
  getSelectedAirportTo(event: Event):void{
    const value = event.target['options'][event.target['options'].selectedIndex].value;
    this.selectedAirportTo = Number(value);
    this.airportToInfo = this.getCityAndCountry(this.selectedAirportTo );
  }

  getCityAndCountry(id:number){
    const airport = this.airports.find(a => a.id ==id);
    return airport.city + ', '+airport.country;
  }

  addNewFlight(addForm: NgForm){
      if(addForm.valid){
        console.log(addForm);
      }
  }

}
