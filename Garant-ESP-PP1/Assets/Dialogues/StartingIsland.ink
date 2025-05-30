INCLUDE Globals.ink

{visitedCentralsland == false: ->firstVisit | -> centralIsland }


=== centralIsland ===
L'île central de l'archipel accueil bien des anciens marins. C'est l'endroit idéal pour ceux qui veulent rester au creux de l'action sans néanmoins en être la cause. #character:Default
+ [Retourner voir l'ancien capitaine du navire]
    -> pirate
+ [Lever l'ancre]
    -> END

=== pirate ===
"Revoilà notre capitaine préféré!" #character:Pirate
+ {visitedTrashIsland && visitedCentralsland && visitedSirenSpire && visitedGoblinIsland && visitedCultistIsland} [J'ai fait le tour de l'archipel!]
    "Ça c'est ce qu'on aime entendre! Viens mon ami, je te paye une bière et tu me raconteras tout ça!"
    ~ endingWorldTraveler = true
    + + [GAME OVER]
        -> END
+ [Retourner au quai]
    -> centralIsland

=== firstVisit ===
"AH AH AH! Te voila enfin! Nul n'est plus méritant de recevoir notre vieux navire! Tu veras que les îles de l'archipel en vaut le détour! Prend ton temps et explore chacune d'entre elle!"
"Tu sais, lorsqu'on a décidé de prendre notre retraite, on savait que la mer allait finir par nous manquer, alors si tu pourrais nous donner un compte-rendu de tes voyages, nous serions reconnaissant! Viens nous voir lorsque tu auras fait le tour de l'archipel!"#character:Pirate
-> firstVisitQuestions
=== firstVisitQuestions ===
+ ["Où devrais-je aller en premier?"]
    "Là où le vent te mène! Tu iras plus vite avec le vent dans tes voiles. Ne t'inquiète pas trop, tu es sûr de trouver quelque chose d'intéressant."
    -> firstVisitQuestions
* [Partir à l'aventure]
    ~ visitedCentralsland = true
    ->END

