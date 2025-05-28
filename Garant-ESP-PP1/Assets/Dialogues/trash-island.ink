INCLUDE Globals.ink

-> main

=== main ===
Sur le bord du quai se trouvent des montagnes immenses d'ordures. L'Île Dépotoire accumule les déchet de l'archipel depuis toujours, mais qu'est-ce qu'ils en deviennent? #character:Default

+ [Visiter l'Île Dépotoire]
   { visitedTrashIsland == false:  -> firstVisit | -> trashIsland }
+ [Lever l'ancre]
    -> END

=== trashIsland ===
La petite fille se penche au sol en écrasant le raton-laveur contre son corps. Elle semble perdue dans sa recherche. #character:RaccoonGirl
+ [Interroger la jeune fille]
    -> questions
+ [Retourner au quai]
    -> main

=== questions ===
La petite fille se penche au sol en écrasant le raton-laveur contre son corps. Elle semble perdue dans sa recherche. #character:RaccoonGirl
+ ["Que fais-tu ici?"]
    "Il y a tellement de belles choses sur cette île! Les gens sur les bateaux débarquent pas longtemps d'habitude. Il font juste déposer leurs affaires et comme aucun d'entre eux sont revenus les chercher... Bah, je me sert!
    -> questions
+ ["Où mène la grande porte derrière toi?"]
    "J'en ai aucune idée! Elle est barrée, mais c'est pas grave! Je vis bien à l'extérieur. J'ai seulement à me mettre dans un sac quand il pleut." 
    -> questions
{trashKey:
+ ["J'ai trouvé cette clé."]
    "Waw! Tu crois que ça peut ouvrir la porte?" La petite fille est tellement contente que tu crois que le raton va manquer d'air dans ses bras.
    -> questions
}

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
