import { Component, OnInit } from '@angular/core';
import { AirportBaseModel, PlaneBaseModel, FlightDetail, AirportDetail, PlaneDetail } from '../models/models';
import { FlightService } from '../services/flight.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm, FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-flight-form',
    templateUrl: './flight-form.component.html',
    styleUrls: ['./flight-form.component.css']
})
export class FlightFormComponent implements OnInit {
    pageLoading: boolean = false;
    airports: AirportDetail[] = [];
    planes: PlaneDetail[] = [];
    emptyFlight: FlightDetail = { id: 0, flightFromId: 0, flightToId: 0, planeId: 0, flightDistance: 0, flightComsuption: 0, creationDate: new Date(), flightDuration: 0 };
    currentFlight: FlightDetail;
    emptyPlane: PlaneDetail = { id: 0, name: '', speed: 0, comsumptionEffort: 0, comsumptionRate: 0 };
    emptyAirpot: AirportDetail = { id: 0, name: '', city: '', country: '', latitude: 0, longitude: 0 };
    SelectedAirportTo: AirportDetail;
    airportFromInfo: string = '';
    airportToInfo: string = '';
    speed: number = 0;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private flightService: FlightService) { }

    ngOnInit() {
        const flightIdParam = this.route.snapshot.paramMap.get("id");
        const flightId = Number(flightIdParam);
        if (flightId == 0) {
            this.currentFlight = this.emptyFlight;
            this.pageLoading = true;
        } else {
            this.flightService.getFlightById(flightId).subscribe(
                (data: FlightDetail) => {
                    this.currentFlight = data;
                    this.pageLoading = true;
                },
                (error: any) => console.log(error)
            );

        }

        this.flightService.getAllAirports().subscribe(
            (data: AirportDetail[]) => {
                this.airports = data;
            },
            (error: any) => {
                console.log(error);
            }
        );
        this.flightService.getAllPlanes().subscribe(
            (data: PlaneDetail[]) => {
                this.planes = data;
            },
            (error: any) => {
                console.log(error);
            }
        )
    }


    getSelectedAirportFrom(event: Event): void {
        const value = event.target['options'][event.target['options'].selectedIndex].value;
        const nmberId = Number(value);
        this.currentFlight.flightFrom = this.getAirport(nmberId);
        this.currentFlight.flightFromId = nmberId;
        this.airportFromInfo = this.currentFlight.flightFrom.city + ', ' + this.currentFlight.flightFrom.country;
        this.calculateDistanceAndComsumption();

    }
    getSelectedAirportTo(event: Event): void {
        const value = event.target['options'][event.target['options'].selectedIndex].value;
        const nmberId = Number(value);
        this.currentFlight.flightToId = nmberId;
        this.currentFlight.flightTo = this.getAirport(nmberId);
        this.airportToInfo = this.currentFlight.flightTo.city + ', ' + this.currentFlight.flightTo.country;
        this.calculateDistanceAndComsumption();
    }
    getSelectedPlane(event: Event): void {
        const value = event.target['options'][event.target['options'].selectedIndex].value;
        const nmberId = Number(value);
        this.currentFlight.planeId = nmberId;
        this.currentFlight.plane = this.getPlane(nmberId);
        this.speed = this.currentFlight.plane.speed;
        this.calculateDistanceAndComsumption();
    }

    getAirport(id: number): AirportDetail {
        const airport = this.airports.find(a => a.id == id);
        return (airport == null) ? this.emptyAirpot : airport;
    }
    getPlane(id: number): PlaneDetail {
        const plane = this.planes.find(p => p.id == id);
        return (plane == null) ? this.emptyPlane : plane;
      
    }

    calculateDistanceAndComsumption() {
        if (this.currentFlight.flightFromId == 0 || this.currentFlight.flightToId == 0) return;
        const latfrom = this.currentFlight.flightFrom.latitude;
        const lonfrom = this.currentFlight.flightFrom.longitude;
        const latto = this.currentFlight.flightTo.latitude;
        const lonto = this.currentFlight.flightTo.longitude;

        this.currentFlight.flightDistance = this.distance(latfrom, lonfrom, latto, lonto, 'K');
        if (this.currentFlight.planeId == 0) return;
        this.currentFlight.flightComsuption = ((this.currentFlight.flightDistance / this.currentFlight.plane.speed) * this.currentFlight.plane.comsumptionRate) + this.currentFlight.plane.comsumptionEffort;
    }

    distance(lat1, lon1, lat2, lon2, unit): number {
        if ((lat1 == lat2) && (lon1 == lon2)) {
            return 0;
        }
        else {
            var radlat1 = Math.PI * lat1 / 180;
            var radlat2 = Math.PI * lat2 / 180;
            var theta = lon1 - lon2;
            var radtheta = Math.PI * theta / 180;
            var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
            if (dist > 1) {
                dist = 1;
            }
            dist = Math.acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            if (unit == "K") { dist = dist * 1.609344 }
            if (unit == "N") { dist = dist * 0.8684 }
            return dist;
        }
    }

    addNewFlight(addForm: NgForm) {

        if (addForm.value.flightToId != 0 && addForm.value.flightFromId && addForm.value.planeId) {
            if (this.currentFlight.id == 0) {
                this.flightService.addFlight(this.currentFlight).subscribe(
                    (data: number) => {
                        this.toastr.success('Flight created successful', 'Success',
                            { timeOut: 2000 });
                        this.router.navigate(['/'])
                    },
                    (error: any) => console.log(error)
                );
            } else {
                this.flightService.updateFlight(this.currentFlight).subscribe(
                    (data: void) => {
                        this.toastr.success('Flight updated successful', 'Success',
                            { timeOut: 2000 });
                        this.router.navigate(['/'])
                    },
                    (error: any) => console.log(error)
                );
            }
        }
        this.toastr.error('Flight form is not completed', '',
            { timeOut: 3000 });
    }

}
