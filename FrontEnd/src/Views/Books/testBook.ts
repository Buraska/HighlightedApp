import type { IBook } from "@/Domain/IBook";
import type { IHighlighted } from "@/Domain/Highlighted";
import type { IPreference } from "@/Domain/IPreference";
import type { IFontFace } from "@/Domain/IFontFace";
import type { IAuthor } from "@/Domain/IAuthor";



export default class TestBook{

  constructor() {
  }

  text = "Foreword\n" +
    "\n" +
    "\n" +
    "This book is about how to train anyone—human or animal, young or old, oneself or others—to do anything that can and should be done. How to get the cat on the kitchen table or your grandmother to stop nagging you. How to affect behavior in your pets, your kids, your boss, your friends. How to improve your tennis stroke, your goli game, your math skills, your memory. All by using the principles of training with reinforcement.\n" +
    "\n" +
    "These principles are laws, like the laws of physics. They underlie all learning-teaching situations as surely as the law of gravity underlies the falling of an apple. Whenever we attempt to change behavior, in ourselves or in others, we are using these laws, whether we know it or\n" +
    "\n" +
    "n0t Usually we are using them inappropriately. We threaten, we argue, we coerce, we deprive. We pounce on others when things go wrong and pass up the chance to praise them when things go right. We are harsh and impatient with our children, with each other, with ourselves even; and we feel guilty over that harshness^ We know that with better methods we could accomplish our ends faster, and without causing distress, but we cant conceive of those methods. We are just not attuned to the ways in which modem trainers take advantage of the laws of positive reinforcement.\n" +
    "\n" +
    "Whatever the training task, whether keeping a touryear-old quiet in public, housebreaking a puppy, coaching\n" +
    "\n" +
    "a team, or memorizing a poem, it will go faster, and better, and be more fun, if you know how to use positive reinforcement.\n" +
    "\n" +
    "The laws of reinforcement are simple; you can put the whole business on a blackboard in ten minutes and learn it in a hour. Applying these laws is more of a challenge; training by reinforcement is like a game, a game dependent upon quick thinking.\n" +
    "\n" +
    "Anyone can be a trainer; some people are good at it from the very start. You do not need special qualities of patience, or a forceful personality, or a way with animals or children, or what circus trainer Frank Buck used to call the power of the human eye. You just need to know what you’re doing.\n" +
    "\n" +
    "There have always been people with an intuitive understanding of how to apply the laws of training. We call them gifted teachers, brilliant commanding officers, winning coaches, genius animal trainers. I’ve observed some theater directors and many symphony conductors who are wonderfully skilled at using reinforcement, these gifted trainers don’t need a book to be able to take advantage of the laws that affect training. For the rest of us, however, those of us muddling along with an uncontrolled pet or at cross-purposes with a child or co-worker a knowledge of how reinforcement really works can be a godsend.\n" +
    "\n" +
    "Reinforcement training is not a system of rewards and punishments by and large modern trainers don’t even use those words. Rewards and punishments usually occur after an act is completed; often, as in criminal justice, long afterward They may or may not affect behavior in the future. They certainly can’t affect behavior that has already taken place. Reinforcement—whether \"positive ” something to be sought after, like a smile or a pat, or negative, something to be avoided, like a tug on a leash or a frown—occurs while the behavior the trainer wishes\n" +
    "\n" +
    "to affect is going on. Correctly timed, reinforcement training works: It changes the behavior.\n" +
    "\n" +
    "I first learned about training with positive reinforcement in Hawaii, where in 1963 I signed on as head dolphin trainer at an oceanarium, Sea Life Park. I had trained dogs and horses by traditional methods, but dolphins were a different proposition; you cannot use a leash or a bridle or even your fist on an animal that just swims away. Positive reinforcement—primarily a bucket of fish—was the only tool we had.\n" +
    "\n" +
    "A psychologist outlined for me the principles of training by reinforcement. The art of applying those principles I learned from working with the dolphins. Schooled as a biologist, and with a lifelong interest in animal behavior, I found myself fascinated, not so much with the dolphins as with what could be communicated between us—from me to the animal and from the animal to me—during this kind of training. I applied what I'd learned from dolphin training to the training of other animals. And I began to notice some applications of the system creeping into my daily life. For example, I stopped yelling at my kids, because I was noticing that yelling didn't work. Watching for behavior I liked, and reinforcing it when it occurred, worked a lot better and kept the peace too.\n" +
    "\n" +
    "There is a solid body of scientific theory underlying the lessons I learned from dolphin training. We shall go considerably beyond theory in this book, since as far as I know, the rules for applying these theories are largely undescribed by science and in my opinion often misapplied by scientists. But the fundamental laws are well established and must be taken into account when training.\n" +
    "\n" +
    "This body of theory is variously known as behavior modification, reinforcement theory, operant conditioning, behaviorism, behavioral psychology, and so on: the branch\n" +
    "\n" +
    "of psychology largely credited to Harvard professor B. F. Skinner.\n" +
    "\n" +
    "I know of no other modern body of scientific information that has been so vilified, misunderstood, misinterpreted, overinterpreted, and misused. The very name of Skinner arouses ire in those who champion “free will” as a characteristic that separates man from beast. To people schooled in the humanistic tradition, the manipulation of human behavior by some sort of conscious technique seems incorrigibly wicked, in spite of the obvious fact that we all go around trying to manipulate one another's behavior all the time, by whatever means come to hand.\n" +
    "\n" +
    "While humanists have been attacking behaviorism and Skinner himself with a fervor that used to be reserved for religious heresies, behaviorism has swelled into a huge branch of psychology, with university departments, clinical practitioners, professional journals, international congresses, graduate-studies programs, doctrines, schisms, and masses and masses of literature.\n" +
    "\n" +
    "And there have been benefits. Some disorders—autism, for example—seem to respond to shaping and reinforcement as to no other treatment. Many individual therapists have been extremely successful in solving the emotional problems of patients by using behavioral techniques. The effectiveness, at least in some circumstances, of simply altering behavior rather than delving into the origins of the behavior has contributed to the rise of family therapy, in which every family member's behavior is looked at, not just the behavior of the one who seems most obviously in distress. This makes eminent good sense.\n" +
    "\n" +
    "Teaching machines and programmed textbooks derived from Skinnerian theory were early attempts to shape learning step by step and to reinforce the student for correct responses. These early mechanisms were clumsy but led directly to CAI, Computer-Assisted Instruction, which is great fun because of the amusing nature of the\n" +
    "\n" +
    "reinforcements (fireworks, dancing robots) and highly effective because of the computers perfect timing. Reinforcement programs using tokens or chits that can be accumulated and traded for candy, cigarettes, or privileges have been established in mental hospitals and other institutions. Self-training programs for weight control and other habit changes abound. And biofeedback is an interesting application of reinforcement to training of physiological responses.\n" +
    "\n" +
    "Academicians have studied the most minute aspects of conditioning. One finding shows, for example, that if you make a chart to keep track of your progress in some self-training program, you will be more likely to maintain new habits if you solidly fill in a little square every day on the chart, rather than just putting a check mark in the square.\n" +
    "\n" +
    "This absorption with detail has valid psychological purposes, but I have not been able to find much good training in it. Training is a loop, a two-way communication in which an event at one end of the loop changes events at the other, exactly like a cybernetic feedback system; yet many psychologists treat their work as something they do to a subject, not with the subject. To a real trainer, the idiosyncratic and unexpected responses any subject can give are the most interesting and potentially the most fruitful events in the training process; yet almost all experimental work is designed to ignore or minimize individualistic responses. Devising methods for what Skinner named “shaping,” the progressive changing of behavior, and carrying out those methods, is a creative process. Yet the psychological literature abounds with shaping programs that are so unimaginative, not to say ham-handed, that they constitute in my opinion cruel and unusual punishment. Take, for example, in one recent journal, a treatment for bed-wetting that involved not only putting “wetness” sensors in the child’s bed but having the thera\n" +
    "\n" +
    "pist spend the night with the child! The authors had the grace to say apologetically that it was rather expensive for the family. How about the expense to the child’s psyche? This kind of “behavioral” solution is like trying to kill flies with a shovel.\n" +
    "\n" +
    "Before going further, I must apologize to any professional behaviorists who are annoyed at my cavalier uses of the vocabulary of reinforcement theory. Skinner’s vocabulary has an elegant specificity —operant conditioning, denoting that the subject is the “operator,” as it were, not just a passive participant; successive approximation, suggesting the stair-step nature of the shaping procedure. In teaching training, however, I find that people balk at this unfamiliar verbiage; to get it across you would have to teach two things: the work and the proper way to converse about it. Meanwhile, in spreading from university to university, the Skinnerian vocabulary itself has undergone some modification; what some call a conditioned stimulus others call a discriminative stimulus, and still others a slangy “S^.” And hairsplitting redefinitions have proliferated. So I have sacrificed technical precision in favor of a vocabulary I think people can understand.\n" +
    "\n" +
    "Schopenhauer once said that every original idea is first ridiculed, then vigorously attacked, and finally taken for granted. As far as I can see, reinforcement theory has been no exception. Skinner was widely ridiculed years ago for demonstrating shaping by developing a pair of Ping-Pong-playing pigeons. The warm, comfortable, selfcleansing, entertainment-providing crib he built for his infant daughters was derided as an inhumane “baby box,” immoral and heretical. Rumors still go around that his daughters went mad, when in fact both of them are successful professional women and quite delightful people. Finally, nowadays many educated people treat reinforcement theory as if it were something not terribly important that they have known and understood all along. In fact\n" +
    "\n" +
    "most people don’t understand it, or they would not behave so badly to the people around them.\n" +
    "\n" +
    "In the years since my dolphin-training experiences, I have lectured and written about the laws of reinforcement in academic and professional circles as well as for the general public. Ive taught this kind of training to high school, college, and graduate students, to housewives and zookeepers, to family and friends. I have watched and studied all kinds of other trainers, from cowboys to coaches, and Ive noticed that the principles of reinforcement training are gradually seeping into our general awareness. Hollywood animal trainers call the use of positive reinforcement ‘‘affection training” and are using these techniques to accomplish behaviors impossible to obtain by force, such as the Merrill Lynch TV commercial of a bull strolling calmly through a china shop. Many Olympic coaches nowadays use positive reinforcement and shaping, instead of relying on old-fashioned browbeating, and have achieved notable improvements in performance.\n" +
    "\n" +
    "Nowhere, however, have I found the rules of reinforcement theory written down so that they could be of use in immediate practical situations. So here they are, explained in this book as I understand them and as I see them used and misused in real life.\n" +
    "\n" +
    "Reinforcement training does not solve all problems—it will not fatten your bank account, it cannot save a bad marriage, and it will not overhaul serious personality disorders. Some situations, such as a crying baby, are not training problems and require other lands of solutions. Some behaviors, in animals and people, have genetic components that may be difficult or impossible to modify by training. Some problems are not worth the training time. But with many of life’s challenges, tasks, and annoyances, correct use of reinforcement can help.\n" +
    "\n" +
    "Practicing the use of positive reinforcement in one situation may induce you to apply it in others. As a dol\n"

  fontFace: IFontFace = {name: "arial"}

  preference: IPreference = {fontSize: 16, fontFace: this.fontFace}

  author: IAuthor = {
  name: "Ivan Vessolom",
  description: "He was a stranger boy. Nothing can be so lazy but he could even if something happen he do his staff and struggel with the skyies like a bog"
}

  book: IBook = {
  title: "Tassssssssssssssssssssssssssm",
  symbolTotal: 10000,
  currentSymbol: 5320,
  isFinished: false,
  content: this.text,
  highlighteds: [],
  preference: this.preference,
  author: this.author}

  highlighteds: IHighlighted[] = [
    {content: "Abrakadabra",
    orderNo: 0,
    startAt: 0,
    endAt: 0,
    book: this.book,
    highlightedType: {name: "quote"},
    likes: []},
    {
      content: "",
      comment: "I think some abrakabammamamamammama likes ltiaedawI think some abrakabammamamamammama likes ltiaedawI think some abrakabammamamamammama likes ltiaedaw",
      orderNo: 0,
      startAt: 0,
      endAt: 0,
      book: this.book,
      highlightedType: {name: "note"},
      likes: []
    },

    {
      content: "MenoMuze",
      comment: "something about memory",
      orderNo: 0,
      startAt: 0,
      endAt: 0,
      book: this.book,
      highlightedType: {name: "dictionary"},
      likes: []}
  ]

}
