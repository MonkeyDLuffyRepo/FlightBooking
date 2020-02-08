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