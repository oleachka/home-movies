import { Component, Input, EventEmitter, Output, OnInit } from "@angular/core";


@Component({
    selector: "mv-star",
    templateUrl: "./star.component.html",
    styleUrls: ["./star.component.css"]
})
export class StarComponent{
    @Input() ratingValue: number = 0;
    @Input() starRatingId: string = "";    

    @Output() ratingClicked: EventEmitter<number> = new EventEmitter<number>();
   
    onClick(value: number): void {        
        this.ratingValue = value;
        this.ratingClicked.emit(this.ratingValue);
    }
}
//https://codepen.io/jamesbarnett/pen/vlpkh?editors=1100