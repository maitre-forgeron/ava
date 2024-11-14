import { Injectable } from "@angular/core";

@Injectable({providedIn: "root"})
export class CardClassGeneratorService {
    computateCardClass(): string {
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