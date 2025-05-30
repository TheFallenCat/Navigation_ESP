INCLUDE Globals.ink

-> main

=== main ===
Sur le bord du quai se trouvent des montagnes immenses d'ordures. L'Île Dépotoire accumule les déchet de l'archipel depuis toujours, mais qu'est-ce qu'ils en deviennent? #character:Default

+ [Visiter l'Île Dépotoire]
   { visitedTrashIsland == false:  -> firstVisit | -> trashIsland }
+ [Lever l'ancre]
    -> END

=== trashIsland ===
La petite fille se penche au sol en écrasant le raton-laveur contre son corps. Elle semble perdue dans sa recherche. Un énorme porte en bois se trouve derrière elle. #character:RaccoonGirl

+ [Interroger la jeune fille]
    -> raccoonGirl

* {trashKey == true && openedTrashDoor == false} [Déverrouiller la porte]
    ~ openedTrashDoor = true
    -> entrerGrotte

* {openedTrashDoor == true} [Entrer dans la grotte]
    -> entrerGrotte

+ [Retourner au quai]
    -> main


=== entrerGrotte ===
Tu entre dans la grotte qui était dissimulé derrière la porte de l'île. À l'intérieur, un autel imposant domine presque la totalité de la caverne. #character:Default
{sirenOrb == false: 
Sur le dessus de l'autel se trouve un perle magnifique.
* [Prendre la perle]
    ~ sirenOrb = true
    -> afterOrb
}
+ {abyssStatue == true} [Mettre la statuette sur l'autel]
    Les murs de la grotte grincent et tremble à l'instant où tu déposes la statuette sur l'autel. Tu as le sentiment étrange au fond de ton être que quelque chose de terrible est sur le point de se produire.
    La statuette émane soudainement une lumière aveuglante. Tu fermes les yeux, mais pourtant tu le vois. Non, pas la lumière, mais la chose qui dévorera le monde. L'Abysse.
    ~ endingDoomsdayRitual = true
    + + [GAME OVER]
        -> END
+ [Sortir de la grotte]
    -> trashIsland

=== afterOrb ===
La perle brillent dans tes mains. Sa teinte crémeuse est presque hypnotisante. Elle veut sortir d'ici.
+ [Sortir de la grotte]
    -> trashIsland
    
=== raccoonGirl ===
-> raccoonGirlQuestions
=== raccoonGirlQuestions ===
+ ["Que fais-tu ici?"]
    "Il y a tellement de belles choses sur cette île! Les gens sur les bateaux débarquent pas longtemps d'habitude. Il font juste déposer leurs affaires et comme aucun d'entre eux sont revenus les chercher... Bah, je me sert!
    -> raccoonGirlQuestions
+ {openedTrashDoor == false} ["Où mène la grande porte derrière toi?"]
    "J'en ai aucune idée! Elle est barrée, mais c'est pas grave! Je vis bien à l'extérieur. J'ai seulement à me mettre dans un sac quand il pleut." 
        -> raccoonGirlQuestions
    + + {trashKey} ["J'ai trouvé cette clé."]
        "Waw! Tu crois que ça peut ouvrir la porte?" La petite fille est tellement contente que tu crois que le raton va manquer d'air dans ses bras.
        -> raccoonGirlQuestions
    
+ [Retour]
    -> trashIsland

=== firstVisit ===
En mettant le pied sur le ponton, l'odeur de vidange est insuportable. Plusieurs membres d'équipage qui souffraient déjà du mal de mer se vident les trippes par dessus bord. Pour le bien être de tous, mieux vos y aller seul.
    ...Derrière une montagne de détritus, tu entends soudainement le bruit d'une canne de conserve qui tombe.
* [Vite! Attrape la source du bruit!] "Je te tiens!"
    Tu pivotes derrière le tas d'ordure et tend ta main lorsque tu te fais soudainement mordre le doigt! un raton-laveur, cruellement restreint dans les bras d'une jeune fille, s'accroche férocement à ta main jusqu'à temps que la petite le force à lâcher prise.
    "Arrête de faire bobo!" La jeune fille étrangle presque la pauvre bête qui ne peux rien faire—sauf lui obéir. "Avez-vous tous vos doigts? Attendez je vais regarder par terre s'ils ont tombés."
* [Avance avec prudence.] Mais qu'est-ce que...
    Tu pivote derrière le tas d'ordure et observe une petite fille fouiller dans les déchets avec ses pieds. Ses bras sont occupés à tenir un petit raton-laveur qui semble vouloir s'échapper à tout prix. Elle te remarque approcher et positionne l'animal entre vous deux. "Oh non! c'est mes trésors!" Elle t'observe de haut en bas, et lorsque son analyse ne revèle aucun désir pour l'abondance de déchets qui vous entour, elle rabaisse son raton de combat et reporte son attention sur ce qui l'entoure.
- ~ visitedTrashIsland = true
-> trashIsland
