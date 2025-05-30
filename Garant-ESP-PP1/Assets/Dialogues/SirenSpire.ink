INCLUDE Globals.ink

-> main

=== main ===
Tu approches d’un îlot rocheux, à peine assez grand pour qu’un seul être s’y tienne. Une silhouette féminine est assise sur un rocher au centre — sa peau luit comme une perle humide, et ses yeux te percent de loin. #character:Siren

+ [Accoster au Rocher]
    { visitedSirenSpire == false: -> firstVisitSiren | -> sirenRock }
+ [Lever l'ancre]
    -> END

=== firstVisitSiren ===
Lorsque tu t’approches, la voix de la créature te frappe comme une vague tiède dans l'esprit.

"Un cœur humain... encore vierge de mon appel. Étonnant."

Elle incline la tête avec curiosité, ses longs cheveux flottant comme des algues.

"Tu n'es pas celui que j'ai brisé. Mais peut-être... tu es celui qui choisira."

- ~ visitedSirenSpire = true
-> sirenRock

=== sirenRock ===
La sirène t’observe, immobile, presque paisible.
{curedCultist == true:
    "Je t’ai vu dans ses souvenirs. L’homme que je garde enchaîné à ma peine. Tu veux qu’il soit libre? Alors donne-moi ce que lui a refusé."
    {abyssStatue == true:
        + [Lui tendre la statuette]
            Ses yeux s’illuminent d’une lumière rougeâtre. Elle tend la main avec une lenteur cérémonielle.
            "Il l’a gardée. Il l’a pleurée. Mais il ne me l’a jamais donnée..."
            Elle prend la statuette contre elle, ferme les yeux, et le chant cesse d’un coup — comme si la mer retenait son souffle.
            "C’est fini. Je n’ai plus besoin de lui. Il est à toi maintenant."
            Dans un frisson de brume, elle disparaît dans la mer.
            Qu'as tu fait?"
    
            ~ endingSirenPlan = true
            + + [GAME OVER]
                -> END
    }
}
+ [Retour]
    -> main