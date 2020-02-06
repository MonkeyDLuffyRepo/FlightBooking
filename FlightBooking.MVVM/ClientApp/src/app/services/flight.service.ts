import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { extend } from 'webdriver-js-extender';

export class FlightBaseModel {
    public id: number;
}

export class FlightDetail extends FlightBaseModel {
    public planeId: number;
    public flightFromId: number;
    public flightToId: number;
    public flightComsuption: number;
    public flightDuration: number;
    public creationDate: Date;
    public flightFrom: AirportDetail;
    public flightTo: AirportDetail;
    public plane: PlaneDetail;

}

export class PlaneBaseModel {
    public id: number;
    public name: string;
}
export class PlaneDetail  extends PlaneBaseModel {
    public comsumptionEffort: number;
    public comsumptionRate: number;
    public speed: number;
}
export class AirportBaseModel {
    public id: number;
    public name: string;
}
export class AirportDetail extends AirportBaseModel {
    public city: string;
    public country: string;
    public latitude: number;
    public longitude: number;
  
}
@Injectable({
  providedIn: 'root'
})
export class FlightService {
    _baseURL: string;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this._baseURL = baseUrl+'/flight' }

    public getAllFlights(): Observable<FlightBaseModel[]> {
        const fullUrl = this._baseURL + '/get-all-flight';

        return this.http.get<FlightBaseModel[]>(
            fullUrl
        );
    }

    public getAllPlanes(): Observable<PlaneDetail[]> {
        const fullUrl = this._baseURL + '/get-all-planes';

        return this.http.get<PlaneDetail[]>(
            fullUrl
        );
    }
    public getAllAirports(): Observable<PlaneDetail[]> {
        const fullUrl = this._baseURL + '/get-all-airport';

        return this.http.get<PlaneDetail[]>(
            fullUrl
        );
    }

    public getFlightById(id: number): Observable<FlightBaseModel> {
        const fullUrl = this._baseURL + '/get-flight-by-id/' + id;

        return this.http.get<FlightBaseModel>(
            fullUrl
        );
    }

    public addFlight(newFlight: FlightBaseModel): Observable<number> {
        const fullUrl = this._baseURL + '/add-flight';

        return this.http.post<number>(
            fullUrl,
            newFlight
        );
    }

    public updateContact(updatedFlight: FlightBaseModel): Observable<void> {
        const fullUrl = this._baseURL + '/update-flight';

        return this.http.put<void>(
            fullUrl,
            updatedFlight
        );
    }

    public deleteFlight(id: number): Observable<void> {
        const fullUrl = this._baseURL + '/delete-flight/' + id;

        return this.http.delete<void>(
            fullUrl
        );
    }

}
