# BlackJack
## Playtesting & Game Review

# Pravidlá

Cieľom hráča je získať kombináciu kariet, ktorej hodnota presahuje hodnotu kariet krupiéra. Je dôležité získať maximálne 21 bodov, inak hráč prehrá (prekročí limit).

Na hru sa používa šesť štandardných balíčkov po 52 kartách. Na účasť v hre musí hráč staviť. Po tom, ako sú všetky stávky podané, dealer rozdá hráčom po dve odkryté karty a sebe jednu odkrytú a jednu zakrytú kartu.

Po dokončení ťahania kariet sa porovnávajú hodnoty konečných kariet.  
- Ak má hráč viac bodov ako dealer → výplata **1:1**  
- Ak sú počty bodov rovnaké (okrem blackjacku) → **remíza**, stávka sa vracia  
- Ak má dealer viac bodov → hráč prehráva  

---

## Čo je blackjack?

Ak prvé dve karty dávajú spolu 21 bodov, ide o **blackjack**.

- Hráč s blackjackom vyhráva **3:2**, ak ho dealer nemá.
- Ak majú obaja blackjack → remíza.
- Ak má dealer otvorenú kartu s hodnotou 10, kontroluje zakrytú kartu.
- Poistenie je možné, ak má dealer tuza.

---

# Logika hry

## Kroky hráča

Ak nikto nemá blackjack, hráč si môže vybrať:

- **Rozdeliť (Split)** – pri dvoch rovnakých kartách.
- **Ešte (Hit)** – vziať ďalšiu kartu.
- **Zdvojnásobiť (Double)** – zdvojnásobiť stávku a vziať presne jednu kartu.
- **Dosť (Stand)** – ukončiť ťahanie.

Ak hráč prekročí 21 → prehráva.

---

## Kroky krupiéra

Krupiér:
- Otvorí zakrytú kartu.
- Ťahá do hodnoty minimálne 17.
- Ak prekročí 21 → všetci aktívni hráči vyhrávajú.

---

## Pravidlo poistenia

Ak má dealer tuza:
- Hráč môže uzavrieť poistenie (½ základnej stávky).
- Ak má dealer blackjack → výplata 2:1.
- Ak nie → poistenie sa prehráva.

---

# Výplaty

| Situácia     | Výplata |
|--------------|---------|
| Bežná výhra  | 1:1     |
| Blackjack    | 3:2     |
| Remíza       | Stávka sa vracia |
| Prekročenie  | Stávka prehráva |
| Poistenie    | 2:1     |

---

# Počítanie bodov

- 2–10 → nominálna hodnota  
- J, Q, K → 10 bodov  
- A (Tuz) → 1 alebo 11 bodov  

---

# Herná skúsenosť

Celkovo bola skúsenosť našej skupiny pozitívna. Pravidlá sú jednoduché a pochopiteľné už po niekoľkých kolách.

Postupne sa však objavila rutina a nedostatok hlbšej stratégie. Po 8–10 kolách hra pôsobila automaticky a menej dynamicky. Veľkú úlohu zohrával faktor šťastia.

V skupine 5 hráčov:
- 1 hráč vyhrával výrazne,
- 1 hráč mierne vyhrával,
- 1 mal striedavý úspech,
- 2 hráči prehrali všetky žetóny.

---

# SWOT analýza – klasická verzia

## Strengths
- Jasný cieľ (21 bez bustu)
- Rýchle kolá
- Strategické rozhodnutia
- Napätie zo skrytej karty dealera
- Učiteľná stratégia

## Weaknesses
- Hráč bustne skôr než dealer
- Variancia
- Repetitívnosť basic strategy
- Rozdiel 3:2 vs 6:5

## Opportunities
- Edukácia (EV, pravdepodobnosť)
- Digitálne verzie
- Turnaje
- Varianty režimov

## Threats
- Hazard stigma
- Regulačné obmedzenia
- Konkurencia
- „Kasíno vždy vyhrá“ efekt

---

# Prvá iterácia – odstránenie remízy

Pri rovnosti si obaja ťahajú karty, kým sa nerozhodne výsledok.

## SWOT – bez remízy

### Strengths
- Každé kolo má víťaza
- Vyššia dynamika
- Silnejší emocionálny moment

### Weaknesses
- Vyššia volatilita
- Zmena matematickej rovnováhy

### Opportunities
- „No Tie Blackjack“
- Diferenciácia na trhu

### Threats
- Riziko nevyváženého house edge

---

# Druhá iterácia – pravidlo „777“

Ak hráč dostane presne tri sedmičky → automatická výhra **5:1**.

## SWOT – verzia 777

### Strengths
- Efekt „wow“
- Dodatočný cieľ
- Marketingový potenciál

### Weaknesses
- Nízka pravdepodobnosť výskytu

### Opportunities
- „Blackjack 777“
- Online animácie
- Úprava výplaty

### Threats
- Narušenie rovnováhy
- Optimalizácia stratégie na 777

---

# Tretia iterácia – odstránenie poistenia

Poistenie bolo odstránené kvôli zjednodušeniu hry.

## SWOT – bez poistenia

### Strengths
- Jednoduchšie pravidlá
- Rýchlejšie tempo
- Beginner-friendly verzia

### Weaknesses
- Menej strategických možností
- Môže pôsobiť ako „oklieštená“ verzia

### Opportunities
- Mobilná verzia
- Kratšie herné session

### Threats
- Očakávania skúsených hráčov

---

# Záver

Každá iterácia zvýšila dynamiku a odlíšila hru od klasickej verzie.  
Najväčší prínos prinieslo pravidlo 777, ktoré zvýšilo emocionálnu hodnotu hry.

Pre zachovanie férovosti je potrebné:
- matematické testovanie,
- výpočet pravdepodobností,
- prípadná kalibrácia výplat.

---

# Autori

- antikman
-
