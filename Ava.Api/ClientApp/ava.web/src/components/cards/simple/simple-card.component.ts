import { Component, input, InputSignal, OnInit } from "@angular/core";
import { CardClassGeneratorService } from "../card-class-generator.service";

@Component({
    selector: 'ava-simple-card',
    standalone: true,
    templateUrl: './simple-card.component.html',
    styleUrl: './simple-card.component.scss',
    providers: [CardClassGeneratorService]
})
export class SimpleCardComponent implements OnInit {
    headerText: InputSignal<string | undefined> = input<string>();

    public class: string = "";

    constructor(private readonly _classGenerator: CardClassGeneratorService) {

    }

    ngOnInit(): void {
        this.class = this._classGenerator.computateCardClass();
    }
}