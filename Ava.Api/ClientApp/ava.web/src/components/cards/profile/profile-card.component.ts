import { Component, input, InputSignal, OnInit } from "@angular/core";
import { CardClassGeneratorService } from "../card-class-generator.service";

@Component({
    selector: 'ava-profile-card',
    standalone: true,
    templateUrl: './profile-card.component.html',
    styleUrl: './profile-card.component.scss',
    providers: [CardClassGeneratorService]
})
export class ProfileCardComponent implements OnInit {
    fullName: InputSignal<string | undefined> = input<string>();
    proffession: InputSignal<string | undefined> = input<string>();
    imageLink: InputSignal<string | undefined> = input<string>();
    description: InputSignal<string | undefined> = input<string>();

    public class: string = "";

    constructor(private readonly _classGenerator: CardClassGeneratorService) {

    }

    ngOnInit(): void {
        this.class = this._classGenerator.computateCardClass();
    }
}
