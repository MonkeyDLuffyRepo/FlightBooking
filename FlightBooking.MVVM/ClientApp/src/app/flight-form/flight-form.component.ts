import { Component, OnInit } from '@angular/core';
import { AirportBaseModel, PlaneBaseModel, FlightDetail, AirportDetail, PlaneDetail } from '../models/models';
import { FlightService } from '../services/flight.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm, FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-flight-form',
  templateUrl: './flight-form.component.html',
  styleUrls: ['./flight-form.component.css']
})
export class FlightFormComponent implements OnInit {
  pageLoading:boolean = false;
  airports : AirportDetail[] = [];
  planes : PlaneDetail[]=[];
  emptyFlight : FlightDetail=  {id:0,flightFromId:0, flightToId:0,planeId:0, flightDistance:0,flightComsuption:0,creationDate : new Date(), flightDuration:0 };
  currentFlight : FlightDetail;
  emptyPlane : PlaneDetail={id:0,name:'',speed:0,comsumptionEffort:0,comsumptionRate:0};
  emptyAirpot :AirportDetail={id:0,name:'',city:'',country:'',latitude:0,longitude:0};
  SelectedAirportTo :AirportDetail ;
  airportFromInfo : string ='';
  airportToInfo : string ='';
  speed:number=0;

  constructor( private route: ActivatedRoute,
    private toastr: ToastrService,
    private flightService:FlightService) { }

  ngOnInit() {
    const flightIdParam = this.route.snapshot.paramMap.get("id");
    const flightId = Number(flightIdParam);
    if(flightId == 0){
      this.currentFlight = this.emptyFlight;
      this.pageLoading = true;
    }else{
     this.flightService.getFlightById(flightId).subscribe(
       (data : FlightDetail) => {
         this.currentFlight = data;
         this.pageLoading = true;
        },
       (error: any) => console.log(error)
     );

    }
    
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
    const nmberId= Number(value);
    this.currentFlight.flightFrom = this.getAirport(nmberId);
    this.currentFlight.flightFromId = nmberId;
    this.airportFromInfo = this.currentFlight.flightFrom.city +', '+this.currentFlight.flightFrom.country
  }
  getSelectedAirportTo(event: Event):void{
    const value = event.target['options'][event.target['options'].selectedIndex].value;
    const nmberId= Number(value);
    this.currentFlight.flightToId = nmberId;
    this.currentFlight.flightTo  = this.getAirport(nmberId);
    this.airportToInfo =  this.currentFlight.flightTo.city +', '+this.currentFlight.flightTo.country
  }
  getSelectedPlane(event:Event):void{
    const value = event.target['options'][event.target['options'].selectedIndex].value;
    const nmberId= Number(value);
    this.currentFlight.planeId = nmberId;
    this.currentFlight.plane = this.getPlane(nmberId);
    this.speed =  this.currentFlight.plane.speed;
  }
  getAirport(id:number):AirportDetail{
    const airport = this.airports.find(a => a.id ==id);
    return (airport == null) ? this.emptyAirpot : airport;
  }
getPlane(id:number):PlaneDetail{
  const plane = this.planes.find(p => p.id == id);
  return (plane == null) ? this.emptyPlane : plane;
}

  addNewFlight(addForm: NgForm){
   
      if(addForm.value.flightToId !=0 &&addForm.value.flightFromId && addForm.value.planeId){
        if(this.currentFlight.id == 0){
          this.flightService.addFlight(this.currentFlight).subscribe(
            (data:number) => console.log('flight added'),
            (error:any)=> console.log(error)
          );
        }else{
          this.flightService.updateFlight(this.currentFlight).subscribe(
            (data:void) => console.log('updated added'),
            (error:any)=> console.log(error)
          );
        }
      }
  }

}
