import { Component, Inject, TemplateRef, ViewChild } from "@angular/core";
import { IMovie, Movie } from "../../models/movie";
import { Http } from "@angular/http";
import { MovieDataService } from "../../services/movie-data.service";


import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Observable } from "rxjs/Observable";
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'
import { FormsModule } from '@angular/forms';
import { Router } from "@angular/router";
import { IGenre } from "../../models/genrets";

@Component({
    selector: "mv-newmovie",
    templateUrl: "./newmovie.component.html"

})
export class NewMovieComponent {
    movie: Movie = new Movie();
    genres: IGenre[];
    private modalRef: BsModalRef;
    @ViewChild('alertTemplate') alertTemplate: TemplateRef<any>;
    constructor(
        private dataService: MovieDataService,
        private modalService: BsModalService,
        private router: Router) {

        dataService.getGenresAsync().subscribe(result => {
            this.genres = result;
        });
    }
    onRatingClicked(value: number) {
        this.movie.rating = value;
    }
    saveClick(): void {

        this.dataService.addMoviesAsync(this.movie).subscribe(result => {
            this.movie = new Movie();
            //this.modalRef = this.modalService.show(this.alertTemplate);
            //setTimeout(() => {
            //    this.modalRef.hide();
            //}, 100);
            this.router.navigateByUrl("home");

        }, error => console.error(error));
    }
}
