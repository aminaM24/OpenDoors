export class NekretninaVM {
  id: number;
  slike:string[];
  brojKvadrata: number;
  brojSoba: number;
  brojKupatila: number;
  brojKreveta: number;
  cijenaPoDanu: number;
  adresa: string;
  avans: boolean;
  lokacijaId: number;
  tipId: number;
  tip: string;
  vlasnikId: number;
  selectedPogodnosti: boolean[] = [];
}
