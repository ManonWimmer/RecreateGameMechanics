INCLUDE globals.ink

#dialogue:LightHouse01


-> Beginning

=== Beginning ===
#camera:0
Sure you don't want to be alone ?  #speaker:Max #audio:Max_sure-you-dont-want-to-be-alone
#camera:1
Have a seat, Pete. #speaker:Chloe #audio:Chloe_have-a-seat-pete
+ [You're in a good mood.]
Bla
-> DONE
+ [My pleasure.]
-> MyPleasure
-> DONE


=== MyPleasure ===
#camera:0
My pleasure. Feels nice out here after all that drama... #speaker:Max #audio:Max_my-pleasure
You really took one for Team Chloe. #speaker:Chloe #audio:Chloe_you-really-took-one-for-team-chloe
#camera:5
I'm not as brave as you. And David is indeed a "step-douche". #speaker:Max #audio:Max_im-not-as-brave-as-you
I'm sorry you had to experience it firsthand. #speaker:Chloe #audio:Chloe_im-sorry-you-had-to-experience-it-firsthand
#camera:3
You have to live with him. Has he always been this way ? #speaker:Max #audio:Max_you-have-to-live-with-him
#camera:1
Ever since my desesperate mom dragged his ass to our home! ...I never trusted David. #speaker:Chloe #audio:Chloe_ever-since-my-desesperate-mom
+ [He freaked out on Kate.]
-> HeFreakedOutOnKate
-> DONE
+ [I should've taken his photo.]
-> DONE

=== HeFreakedOutOnKate ===
#camera:0
He freaked out on poor Kate Marsh today. #speaker:Max #audio:Max_he-freaked-out-on-poor-kate-marsh-today
I know her. She's cool. Only that prick would bully her. #speaker:Chloe #audio:Chloe_i-know-her-shes-cool
#camera:3
He has some kind of weird agenda. #speaker:Max #audio:Max_he-has-some-kind-of-weird-agenda
#camera:1
He has a lot of secret files. Rambo still thinks he's gathering enemy intelligence. #speaker:Chloe #audio:Chloe_he-has-a-lot-of-secret-files
Did you take a peek ? #speaker:Chloe #audio:Chloe_did-you-take-a-peek
+ [I wanted to.]
-> IWantedTo
-> DONE
+ [Not me.]
-> DONE

=== IWantedTo === 
#camera:3
You know I wanted to, but... I realized I have enough mystery in my life. #speaker:Max #audio:Max_you-know-i-wanted-to
#camera:1
I'd like to find out. I bet he's got some serious porn in there. #speaker:Chloe #audio:Chloe_id-like-to-find-out
#camera:4
Ew. #speaker:Max #audio:Max_ew
Good thing you didn't look. #speaker:Chloe #audio:Chloe_good-thing-you-didnt-look
#camera:1
He has a total surveillance fetish. I worry there are spy cams in the house. #speaker:Chloe #audio:Chloe_he-has-a-total-surveillance-fetish
#camera:0
I knew you didn't know! Chloe, your house is under surveillance. #speaker:Max #audio:Max_i-knew-you-didnt-know
#camera:1
What are you talking about ? #speaker: Chloe #audio:Chloe_what-are-you-talking-about
#camera:3
There are cameras all over the house. I saw it on a monitor in the garage. #speaker:Max #audio:Max_there-are-cameras-all-over-the-house
#camera:2
I knew it! He is so hella fucking paranoid. I'll keep this a secret for now... #speaker:Chloe #audio:Chloe_i-knew-it-hes-so-hella-fucking-paranoid
#camera:0
Sometimes ignorance is bliss. #speaker:Max #audio:Max_sometimes-ignorance-is-bliss
#camera:1
No wonder I'm so miserable. Everybody in this town knows everybody's secrets... #speaker:Chloe #audio:Chloe_no-wonder-im-so-miserable
+ [What's Nathan's secret ?]
-> WhatsNathansSecret
-> DONE
+ [Even yours ?]
-> DONE

=== WhatsNathansSecret ===
#camera:3
What's Nathan's secret ? #speaker:Max #audio:Max_whats-nathans-secret
He's an elite asshole who sells bad shit cut with laxative... #speaker:Chloe #audio:Chloe_hes-an-elite-asshole1
and he dosed me with some drug in his room. #speaker:Chloe #audio:Chloe_hes-an-elite-asshole2
What? #speaker:Max #audio:Max_what
I met him in some shithole bar that didn't card me. #speaker:Chloe #audio:Chloe_i-met-him-in-some-shithole-bar1
He was too rich for the place and too wasted. And he kept flashing bills... #speaker:Chloe #audio:Chloe_i-met-him-in-some-shithole-bar2
Just tell me what happened, Chloe. Now. #speaker:Max #audio:Max_just-tell-me-what-happened-chloe
I was an idiot. I thought he was so blazed it would be an easy score. #speaker:Chloe #audio:Chloe_i-was-an-idiot
You needed money that bad? #speaker:Max #audio:Max_you-needed-money-that-bad
Actually, yes. I owe big time. #speaker:Chloe #audio:Chloe_actually-yes-i-owe-big-time1
And I thought I'd have enough for me and Rachel if she showed up... #speaker:Chloe #audio:Chloe_actually-yes-i-owe-big-time2
+ [What about Nathan ?]
-> DONE
+ [How much?]
-> HowMuch
-> DONE

=== HowMuch ===
How much do you owe ? #speaker:Max #audio:Max_how-much-do-you-owe
Three grand plus interest. And before I could get a chunk of that from Nathan... #speaker:Chloe #audio:Chloe_three-grand-plus-interest1
he dosed my drink with some shit... #speaker:Chloe #speaker:Chloe #audio:Chloe_three-grand-plus-interest2
God Chloe, I can't believe this... I mean, I do. Then what? #speaker:Max #audio:Max_god-chloe-i-cant-believe-his
I know I passed out on the floor. #speaker:Chloe #audio:Chloe_i-know-i-passed-out-on-the-floor1
I woke up and that perv was smiling, crawling towards me with a camera... #speaker:Chloe #audio:Chloe_i-know-i-passed-out-on-the-floor2
Go on... #speaker:Max #audio:Max_go-on
Everything was a blur... I tried to kick him in the balls and broke a lamp. #speaker:Chloe #audio:Chloe_everything-was-a-blur1
Nathan freaked, so I managed to bum rush the door and get the hell out. #speaker:Chloe #audio:Chloe_everything-was-a-blur2
Max, it was insane. #speaker:Chloe #audio:Chloe_everything-was-a-blur3
+ [That is so fucked up]
-> ThatIsSoFuckedUp
-> DONE 
+ [I am so furious]
-> DONE

=== ThatIsSoFuckedUp ===
Chloe, that is so fucked up. What did you do then? #speaker:Max #audio:Max_chloe-that-is-so-fucked-up
I figured I would make him pay me to keep quiet. So we met in the bathroom. #speaker:Chloe #audio:Chloe_i-figured-i-would-make-him-pay-me-to-keep-quiet
And he brought a gun. #speaker:Max #audio:Max_and-he-brought-a-gun
That was Nathan's last mistake... #speaker:Chloe #audio:Chloe_that-was-nathans-last-mistake
+ [He's still dangerous]
-> HesStillDangerous
-> DONE
+ [Let's call the police]
-> DONE

=== HesStillDangerous ===
He's still dangerous, Chloe. Not just to you. #speaker:Max #audio:Max_hes-still-dangerous-chloe
Oh, good thing you notified the Principal. I feel safer already... #speaker:Chloe #audio:Chloe_oh-good-thing-you-notified-the-principal
I won't always be there to save you... #speaker:Max #audio:Max_i-wont-always-be-there-to-save-you
You were here today, Max. You saved me! I'm still tripping on that... #speaker:Chloe #audio:Chloe_you-were-here-today-max-you-saved-me1
Seeing you after all these years feels like- #speaker:Chloe #audio:Chloe_you-were-here-today-max-you-saved-me2
Destiny? #speaker:Max #audio:Max_destiny 
If this is destiny, I hope we can find Rachel. I miss her, Max. #speaker:Chloe #audio:Chloe_if-this-is-destiny-i-hope-we-can-find-rachel
This shit-pit has taken away everyone I've ever loved... #speaker:Chloe #audio:Chloe_that-shit-pit-is-taking-away-everyone-ive-ever-loved1
I'd like to drop a bomb on Arcadia Bay and turn it to fucking glass... #speaker:Chloe #audio:Chloe_that-shit-pit-is-taking-away-everyone-ive-ever-loved2
-> DONE



















