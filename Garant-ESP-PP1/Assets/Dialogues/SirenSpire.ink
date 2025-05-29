INCLUDE Globals.ink


Des monolithes de pierre pointe vers le ciel au milieu de la mer. Les vagues se fracassent sur les roches et l'écume s'accumule entre les piliers. À l'approche du navire, tu remarques d'étranges plantes qui pousse sur les rochers{visitedCultistIsland == true:, similaires aux plantes qui se trouvait sur l'île de l'homme mystérieux. Ceci doit être où se trouve la sirène}.

+ [Observer de plus près]
    { visitedSirenSpire == false:  -> firstVisit | -> sirenSpire }
+ [Lever l'ancre]
    -> END
    
=== firstVisit ===
Du dessus d'un des rochers, une figure apparait soudainement. Il s'agit d'une femme magnifique, du moins, si ce n'était pas immédiatement apparant que c'était une sirène. Difficile de juger de magnifique quelque chose qui voudrait probablement te manger. Quoique.
La sirène rampe sur la pierre pour s'approcher et s'adresse à ton équipage.
"" #character:Siren
-> END

=== sirenSpire ===
-> END