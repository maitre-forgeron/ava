import { Component, input, InputSignal, OnInit } from "@angular/core";

@Component({
    selector: 'ava-simple-card',
    standalone: true,
    templateUrl: './simple-card.component.html',
    styleUrl: './simple-card.component.scss'
})
export class SimpleCardComponent implements OnInit {
    headerText: InputSignal<string | undefined> = input<string>();

    public class: string = "";

    ngOnInit(): void {
        this.class = this.computateCardClass();
    }

    private computateCardClass(): string {
        const defaultCardClass = "card";

        const randomCardClass: string = this.getCardClassVariation()[this.randomIntFromInterval(1, 9)];

        let computatedCardClass = `${defaultCardClass} ${randomCardClass}`;

        return computatedCardClass;
    }

    private getCardClassVariation(): string[] {
        return ["card-1", "card-2", "card-3", "card-4", "card-5", "card-6", "card-7", "card-8", "card-9"];
    }

    private randomIntFromInterval(min: number, max: number) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }
}