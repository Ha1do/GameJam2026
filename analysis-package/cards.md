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

Ak prvé dve karty v súčte dávajú 21 bodov, takáto kombinácia sa nazýva blackjack. Ak dealer získa blackjack, všetci hráči prehrávajú, okrem tých, ktorí majú tiež blackjack. Takýto prípad sa považuje za remízu a stávka sa vráti hráčovi. Ak hráč má blackjack a dealer nie, hráč vyhráva a dostane výplatu 3 ku 2 zo svojej stávky. Ak má otvorená karta dealera hodnotu 10 bodov, dealer sa pozrie na svoju zakrytú kartu. V prípade, že sa zozbierala kombinácia blackjack, dealer otvorí svoje karty a kolo hry končí.

---

# Logika hry

## Kroky hráča

Ak dealer ani hráč nemajú blackjack, hráč po rozdaní kariet môže zvoliť jednu z viacerých akcií:
 - „Rozdeliť“. K dispozícii len v prípade, ak má v ruke dve karty rovnakej hodnoty. Hráč zdvojnásobí svoju stávku, rozdelí ruku na dve a dostane dve ďalšie karty. Tieto ruky sa ďalej hrajú nezávisle.
 - „Ešte“. Hráč si vezme ďalšiu kartu. Táto akcia sa môže opakovať, kým súčet bodov neprekročí 21.
 - „Zdvojnásobiť“. Hráč zdvojnásobí svoju stávku, vezme si presne jednu ďalšiu kartu a zafixuje svoje karty.
 - „Dosť“. Hráč zafixuje svoje karty.

Ak hráč nazbieral viac ako 21 bodov, prehráva kolo.

---

## Kroky krupiéra

Potom, čo hráči zafixovali svoje karty, krupiér otvorí zakrytú kartu.
V prípade potreby krupiér berie ďalšie karty, kým suma bodov nedosiahne 17 alebo viac. Ak počet bodov krupiéra prekročí 21, všetci hráči, ktorí neopustili hru, automaticky vyhrávajú, bez ohľadu na počet bodov.

---

## Pravidlo poistenia

Osobitný prípad: ak má dealer otvorenú kartu tuz, hráčom bude ponúknutá dodatočná stávka „poistenie“ vo výške polovice ich základnej stávky. Potom, čo všetci hráči prijmú rozhodnutie, dealer pozrie na svoju zakrytú kartu. V prípade, že dealer má blackjack, na stávky „poistenie“ sa vypláca výhra 2 ku 1. Ak dealer nemá blackjack, stávky „poistenie“ idú do banku a hra pokračuje ako zvyčajne.

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

Pri počítaní bodov sa berú do úvahy hodnoty kariet: karty od 2 do 10 dávajú počet bodov rovný svojej hodnote, karty J (Valet), Q (Kráľovná), K (Kráľ) dávajú 10 bodov. A (Tuz) má hodnotu 1 alebo 11 bodov, v závislosti od toho, pri akej hodnote súčet bodov neprekročí 21.

---

# Herná skúsenosť

Celkovo bola skúsenosť našej skupiny s hrou blackjack pomerne pozitívna: táto kartová hra sa výrazne vyznačuje jednoduchými pravidlami, takže aj hráči, ktorí túto hru nikdy nehrali, pochopili jej podstatu už po prvých dvoch kolách.
Na druhej strane, jednoduchosť pravidiel vytvorila problém rutiny a absencie strategických krokov. Po približne ôsmom až desiatom kole sa hra hrá automaticky a nedáva hráčovi možnosť veľa premýšľať a poraziť krupiéra pomocou zložitej a originálnej stratégie, čo spôsobuje, že hra sa stáva trochu nudnou. Náš tím nevydržal viac ako 20 kôl.
Na pozadí toho vyniká aj ďalšia zvláštnosť, a to závislosť od šťastia. Vzhľadom na to, že v hre chýba možnosť strategického myslenia, hráči sa veľmi často spoliehajú na bežné šťastie. Ako záver, v našom tíme s 5 hráčmi jeden hráč vyhrával veľmi veľa, jeden vyhrával, ale nebol až tak úspešný, jeden mal celý čas striedavý úspech a poslední dvaja prehrali všetky imitačné žetóny.


---

# SWOT analýza – klasická verzia

|  | **Užitočné** | **Škodlivé** |
|---|---|---|
| **Vnútorné atribúty** | **Strengths:**<br>• Jasný cieľ: 21 bez bustu.<br>• Rýchle kolá, vysoká znovuhrateľnosť.<br>• Rozhodnutia: hit/stand/double/split.<br>• Napätie: krupiérova skrytá karta.<br>• Jednoduchý balíček, rýchla príprava.<br>• Učiteľná stratégia (zač. → pokroč.). | **Weaknesses:**<br>• Hráč bustne skôr než krupiér (vnímaná neférovosť).<br>• Výnimky pravidiel → zmätok (soft/hard, split, S17/H17, double).<br>• Variancia → série prehier aj pri dobrej hre.<br>• Basic strategy pôsobí repetitívne.<br>• Výplaty 3:2 vs 6:5 menia férovosť. |
| **Vonkajšie atribúty** | **Opportunities:**<br>• Edukácia: pravdepodobnosť, EV, riziko/bankroll.<br>• Digitál: tutoriál, nápovedy, štatistiky.<br>• Social: best-of-N, mini turnaje.<br>• Režimy/varianty pre sviežosť.<br>• Známa hra → ľahké zapojenie. | **Threats:**<br>• Hazard stigma → zákony/vek/platforomy.<br>• Návykové správanie → etika, reputácia.<br>• Podvádzanie: karty / RNG dôvera.<br>• Silná konkurencia kartoviek.<br>• "Kasíno vždy vyhrá" → odchod hráčov. |

---

# Prvá iterácia zmeny pravidiel hry:

Táto verzia hry odstraňuje možnosť remízy.

Ak hráč aj dealer dosiahnu rovnaký počet bodov, obaja si ťahajú po jednej karte dovtedy, kým sa výsledok nerozhodne. Víťazom sa stáva ten, kto má finálny počet bodov bližší k 21 (resp. nižší bez prekročenia limitu).

Cieľom úpravy je zvýšiť dynamiku hry, eliminovať „neutrálne“ kolá a priniesť rozhodujúci moment v každej partii.


## SWOT analýza verzie bez remízy:

### Strengths
- **Každá hra má víťaza** – žiadne „mŕtve“ kolá bez výsledku.
- **Vyššia dynamika hry** – viac napätia a akcie.
- **Silnejší emocionálny zážitok** – každá remíza sa mení na rozhodujúci moment.
- **Zvýšený obrat stávok** – žiadne vracanie stávky pri rovnosti.

### Weaknesses
- Zvýšená volatilita pre hráča (remíza sa mení na výhru alebo prehru).
- Možné negatívne vnímanie zo strany konzervatívnych hráčov.
- Zložitejšia matematická analýza dopadu na house edge.
- Potenciálne spomalenie hry pri častých rovnostiach.


### Opportunities
- Možnosť pozicionovať ako **„Blackjack bez remíz“** alebo **„No Tie Blackjack“**.
- Vhodné pre dynamickú online verziu (automatický rozhodovací ťah).
- Možnosť jemného nastavenia RTP cez iné pravidlá (napr. 777).
- Diferenciácia od klasického blackjacku na trhu.


### Threats
- Riziko nevyváženého house edge bez dôkladného testovania.
- Potenciálne regulačné požiadavky pri zmene základnej mechaniky.
- Skúsení hráči môžu analyzovať novú optimálnu stratégiu.
- Ak bude hra príliš tvrdá, môže klesnúť retencia hráčov.

## Zhodnotenie iterácie

Odstránenie remízy predstavuje zásadnú zmenu základnej mechaniky hry. Každé kolo má jednoznačný výsledok – výhru alebo prehru – čo zvyšuje dynamiku, tempo a emocionálne napätie.

Z pohľadu hráčskeho zážitku ide o výrazne akčnejšiu verziu blackjacku, kde sa eliminuje pocit „strateného kola“. Každá situácia rovnosti sa mení na rozhodujúci moment, čo môže zvýšiť angažovanosť hráčov.

Na druhej strane však dochádza k zvýšeniu volatility a k zmene matematickej rovnováhy hry. Keďže remíza v klasickom blackjacku znižuje varianciu, jej odstránenie môže mať významný dopad na house edge. Bez dôkladnej simulácie existuje riziko nevyváženosti hry.

Celkovo možno túto iteráciu hodnotiť ako:
- Dynamickú a napínavejšiu než klasická verzia
- Marketingovo odlíšiteľnú („Blackjack bez remíz“)
- Matematicky citlivú – vyžaduje presné testovanie a kalibráciu

---

# Druhá iterácia – pravidlo „777“

## Pravidlo „777“

Do hry bolo pridané špeciálne bonusové pravidlo **„777“**:
- Ak hráč dostane **presne tri karty s nominálnou hodnotou 7**, automaticky vyhráva s výplatou **5:1**.
- Ak hráč aj dealer získajú kombináciu troch sedmičiek, vznikne remíza. Tento prípad je riešený podľa pravidiel definovaných v prvej iterácii (dodatočné ťahanie kariet až do rozhodnutia).
V prípade konečného víťazstva hráča získa hráč výhru **5:1**.
V prípade víťazstva dealera hráč prehráva svoju stávku.

Toto pravidlo zavádza do hry bonusový mechanizmus, ktorý rozširuje pôvodnú štruktúru blackjacku bez zásadnej zmeny základnej mechaniky hry.

## SWOT analýza verzie s pravidlom „777“

### Strengths
- **Efekt „wow“** – pravidlo 777 prináša vzácny a emocionálne silný moment.
- **Zvýšený záujem hráčov** – vzniká dodatočný cieľ (získať kombináciu 777).
- **Vyššia atraktivita hry** – možnosť nadštandardnej výhry (5:1) zvyšuje napätie.
- **Marketingový potenciál** – hra pôsobí dynamickejšie a originálnejšie.

### Weaknesses
- **Nízka pravdepodobnosť výskytu** – pri viacerých balíčkoch môže byť kombinácia 777 veľmi zriedkavá.

### Opportunities
- Možnosť vytvoriť variant hry pod názvom **„Blackjack 777“** alebo **„Blackjack Extreme“**.
- Vhodné pre online verziu s animáciou a zvukovým efektom bonusu.
- Možnosť vyváženia výplaty (5:1 možno zmeniť po testoch).


### Threats
- **Narušenie matematickej rovnováhy hry** – hráč môže získať vysokú výhru bez výrazne vyššieho rizika.
- Keďže kombinácia 7-7-7 predstavuje hodnotu 21, hráč pri dvoch sedmičkách riskuje podobne ako pri bežnej hre, ale má potenciál výrazne vyššej výhry.
- Ak je výplata 5:1 príliš vysoká vzhľadom na pravdepodobnosť, môže to zvýšiť výhodu hráča nad akceptovateľnú úroveň.
- Skúsení hráči môžu začať optimalizovať stratégiu špeciálne na získanie 777.

## Zhodnotenie iterácie

Pravidlo „777“ zvyšuje atraktivitu hry a pridáva emocionálny prvok bez zásadného narušenia základnej mechaniky blackjacku.

Na udržanie bilancie je však potrebné:

- Vypočítať pravdepodobnosť výskytu kombinácie 777.
- Prípadne upraviť výplatný pomer.

Celkovo ide o zmenu, ktorá zvyšuje zábavnosť hry, no vyžaduje matematické testovanie, aby sa zachovala férovosť a vyváženosť.

---

# Tretia iterácia zmeny pravidiel hry

Keďže v druhej iterácii bolo pridané pravidlo **„777“**, v tretej sme odstránili **poistenie**, aby sme zjednodušili pravidlá a znížili počet „vedľajších rozhodnutí“ počas rozdávania. To znamená, že keď má dealer otvoreného tuza, nevzniká dodatočná možnosť uzavrieť poistnú stávku – kolo pokračuje štandardnými akciami hráča (ešte/dosť atď.) bez akéhokoľvek „poistného“ kroku.

Cieľom bolo zjednodušiť priebeh rozdávania a urýchliť tempo hry tým, že sa odstránilo zbytočné vedľajšie rozhodnutie v prípade, že dealer má tuz, aby sa po pridaní pravidla 777 pozornosť sústredila na hlavné akcie hráča.


## SWOT analýza verzie bez poistenia

### Strengths
- **Jednoduchšie pravidlá:** ľahšie vysvetľovať a rýchlejšie učiť nových hráčov.
- **Rýchlejší a čistejší herný cyklus:** menej prestávok/podkrokov počas rozdávania.
- **Viac zamerania na jadro hry:** rozhodnutia hráča sa zredukujú na hlavné akcie („Ešte“ / „Dosť“ / „Zdvojnásobiť“ / „Rozdeliť“), a nie na dodatočné stávky.
- **Emocionálny „háčik“ zostáva cez 777:** je tu dodatočný cieľ/moment radosti bez komplikovania základných rozhodnutí.

### Weaknesses
- **Menej variability výberu:** skúseným hráčom môže chýbať dodatočná možnosť.
- **Menší pocit kontroly v situácii s esom krupiéra:** hráč nemôže „reagovať stávkou“ na potenciálny blackjack krupiéra.
- **Riziko vnímania ako „nie klasický blackjack“:** časť publika očakáva poistenie ako štandard.

### Opportunities
- **Pozicionovanie ako „beginner-friendly blackjack“:** rýchlo sa do toho dostať, ľahko hrať, menej chybných rozhodnutí.
- **Lepšia vhodnosť pre mobilnú/online verziu:** kratšie a dynamickejšie sedenia.

### Threats
- **Balans/očakávania:** nespokojnosť časti skúsených hráčov, ktorí sú zvyknutí na plnú sadu klasických možností.
- **Porovnávanie s „originálom“:** môžu kritizovať ako „orezanú“ verziu.

## Zhodnotenie iterácie

V dôsledku toho zrušenie poistenia urobilo pravidlá ešte jednoduchšími a zrozumiteľnejšími pre nováčikov a urýchlilo tempo rozdávania kariet, avšak pre skúsených hráčov to môže vyzerať ako zbytočné zjednodušenie, pretože klasický blackjack nie je príliš zložitý a zrušenie poistenia dodatočne znižuje rozmanitosť rozhodnutí a odstraňuje jednu zo strategických možností, ktorých v blackjacku aj tak nie je veľa.

---

# Záver

Počas playtestingu sme zistili, že blackjack je hra s veľmi jednoduchými pravidlami, ktorú hráči pochopia už po niekoľkých kolách. Hra je rýchla a dynamická, no po dlhšom hraní môže začať pôsobiť trochu repetitívne, pretože veľká časť výsledku závisí od náhody.

Navrhnuté iterácie sa snažili hru mierne oživiť a pridať nové momenty napätia bez zásadného narušenia základnej mechaniky. Zmeny ako odstránenie remízy, zavedenie bonusového pravidla „777“ alebo odstránenie poistenia ukazujú, že aj malé úpravy pravidiel môžu výrazne ovplyvniť tempo hry, hráčsky zážitok a strategické rozhodovanie. Zároveň je dôležité si uvedomiť, že klasický blackjack je hra, ktorej pravidlá sa vyvíjali dlhodobo a sú relatívne dobre vyvážené, preto aj malé zmeny môžu ovplyvniť pravdepodobnosti výhier a celkový herný balans.

Celkovo možno povedať, že vykonané úpravy robia hru dynamickejšou a jednoduchšou na pochopenie, pričom zachovávajú základný charakter blackjacku. Takáto verzia môže byť atraktívna najmä pre príležitostných alebo nových hráčov, no na potvrdenie jej vyváženosti by bolo vhodné vykonať ďalší playtesting alebo matematické simulácie.

---

# Autori

- Ivan Honcharuk
- Vladyslav Stakhov
- Anton Allahveriiev
- Maksym Zvarych
- Illia Zhyzhyn
