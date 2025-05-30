INCLUDE Globals.ink

-> main

=== main ===
Le quai est bancal, mais assez solide pour accoster. Sur l’île, un étal couvert de tissus criards déborde d’objets poussiéreux et de babioles douteuses. Un petit gobelin aux oreilles énormes s’agite derrière le comptoir, tentant d’attirer ton attention. 

+ [Visiter le Marché Crochu]
    { visitedGoblinIsland == false: -> firstVisitGoblin | -> goblinMarket }
+ [Lever l’ancre]
    -> END

=== firstVisitGoblin ===
Le gobelin bondit sur le comptoir dès que tu poses un pied sur l’île.
"CLIENT!! ENFIN!! Viens, viens! Je jure sur mes dents de lait que j’ai l’affaire du siècle pour toi!"
Il te tire presque par le bras pour t’approcher de son bazar. Une vieille clé rouillée pend au bout d’un fil.
"Tu la veux, hein? Dis oui. Tu devrais. Je t’en fais un prix. En fait... je te la DONNE!"#character:Goblin

- ~ visitedGoblinIsland = true
-> goblinMarket

=== goblinMarket ===
Le gobelin t’observe avec un mélange d’espoir et de désespoir. Derrière lui, des boîtes s'empilent, certaines bougent encore. {trashKey == false:Mais son regard revient toujours vers la vieille clé suspendue.}
-> goblinQuestions
=== goblinQuestions ===
{trashKey == false:
    + [Accepter la clé]
        Le gobelin te la fourre dans les mains avant que tu puisses protester.

        "C’est à toi maintenant! Va t’faire maudire, ou deviens riche! Moi j’ai fini!"

        Il part en courant derrière son stand, comme s’il s’était débarrassé d’un poids.

        ~ trashKey = true
        -> afterKey
    + [Refuser la clé]
        "Non?? TU VAS LE REGRETTER! C’est gratuit!! C’est une opportunité en or!! GAAAH!!"
        Il se roule par terre, déchirant une ancienne facture à pleines dents.
        -> goblinQuestions
}
+ [Retourner au quai]
    -> main

=== examineGoods ===
Tu fouilles quelques boîtes : des pantoufles trop petites, des fioles vides prétendument invisibles, et un chapeau qui hurle lorsqu’on le touche.

Rien d’utile. Tout semble faux… sauf cette clé.

+ [Retour]
    -> goblinMarket

=== afterKey ===
La clé est étrange au toucher. Elle pulse légèrement, comme un petit cœur froid. Tu ignores encore ce qu’elle ouvre — mais elle attend.

-> goblinMarket
