INCLUDE Globals.ink

-> main

=== main ===
Un silence lourd plane autour de l’Île des Sombrés. La mer y est noire et d'étranges plantes poussent sur la côte. Un campement est visible au loin. #character:Default

+ [Accoster sur l'Île des Sombrés]
    { visitedCultistIsland == false: -> firstVisitMirror | -> cultistIsland }
+ [Lever l’ancre]
    -> END

=== cultistIsland ===
Un homme au teint cireux t’attend sur la plage, immobile. Il a plus l'air d'un molusque que d'un homme sous sa cape et ses yeux brillent d’un éclat surnaturel. #character:Cultist
+ [Parler à l’homme]
    -> talkCultist
+ {sirenOrb == true && curedCultist == false} [Donner la Perle des Écumes]
    "C'est elle... Je la sens. Le chant s’éloigne..." L'homme tremble en tenant la perle dans ses paumes. Sa peau rugueuse s'effritent doucement, et ses larmes noircies roulent sur ses joues.
    -> cureEnding

+ {curedCultist == true && abyssStatue == false} [Aller au bassin]
    -> beachRuins

+ [Retourner au quai]
    -> main

=== firstVisitMirror ===
En posant pied sur la terre sombre, tu entends un chant lointain. Il est beau... mais douloureux. Il semble se glisser dans tes pensées. Un homme sort lentement de la brume.

"Tu l’entends aussi... n’est-ce pas?" murmure-t-il. "Elle chante encore. Elle me retient... mais je peux être sauvé. Il me faut la Perle des Écumes." #character:Cultist
+ [Continuer]
    - ~ visitedCultistIsland = true
    -> cultistIsland

=== talkCultist ===
-> talkCultistOptions

=== talkCultistOptions ===
+ ["Qui êtes-vous?"]
    "Un naufragé... mais pas des flots. Mon cœur a coulé bien avant mon navire. La sirène m’a ensorcelé. Je sens ses pensées dans ma chair. {sirenOrb == false:Mais si je récupère la Perle des Écumes, je pourrai rompre le lien."}
    -> talkCultistOptions
    
+ {sirenOrb == false} ["Où trouver cette perle?"]
    "Là où tout est oublié et jeté. "
    -> talkCultistOptions
+ [Retour]
    -> cultistIsland

=== beachRuins ===
Au centre de l’île se trouve un bassin. L’eau stagnante est limpide, mais une étrange vibration y résonne. Tu ressens une présence... qui t’observe.

+ [Plonger dans le bassin]
    -> diveEvent
+ [Revenir plus tard]
    -> cultistIsland

=== diveEvent ===
Tu retiens ton souffle et entres dans l’eau glacée. La lumière faiblit à mesure que tu descends...  
...tout au fond, un éclat pâle t’attire : une statuette de pierre pâle.

Mais une silhouette ondule dans l’ombre.

+ [Tenter de prendre la statuette rapidement]
    Tu saisis la perle, mais la douleur te transperce le crâne. Un cri lointain t’arrache à toi-même, puis le silence.
    Tu t’éveilles sur la rive, grelottant. Mais tu tiens la statuette.
    ~ abyssStatue = true
    + + [Retour]
        -> cultistIsland

+ [Reculer et fuir]
    Tu remontes précipitamment. Le chant résonne plus fort, furieux, comme si quelque chose s'était échappé.
    + + [Retour]
        -> cultistIsland

=== cureEnding ===
L'homme tombe à genoux, libéré de l’emprise. "Tu m’as sauvé… Les changements prendront un certains temps, mais à l'aube, je serais un homme.."
"La sirène qui m'a ensorcelé... Elle l'a fait par le billet d'une statuette. Elle m'avait promis qu'elle me donnerait ce que je souhaitais. Je l'ai jeté dans le bassin sur cette île. Je n'y toucherais pas si j'étais toi."
~ curedCultist = true
+ [Continuer]
    -> cultistIsland
