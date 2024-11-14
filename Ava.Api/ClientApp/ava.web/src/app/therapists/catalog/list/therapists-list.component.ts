import { Component, Input, OnInit } from "@angular/core";
import { Therapist } from "../../../models/therapist.model";
import { ProfileCardComponent } from "../../../../components/cards/profile/profile-card.component";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { TherapistService } from "../../../services/therapist.service";
import { Observable } from "rxjs";

@Component({
    selector: 'ava-therapists-list',
    standalone: true,
    templateUrl: './therapists-list.component.html',
    styleUrls: ['./therapists-list.component.scss'],
    imports: [ProfileCardComponent, CommonModule],
    providers: [TherapistService]
})
export class TherapistsListComponent implements OnInit {
    therapists: Observable<Therapist[]> | undefined;

    constructor(private readonly _therapistService: TherapistService) {
    }

    ngOnInit(): void {
        this.therapists = this._therapistService.getAllTherapists();
    }
}
