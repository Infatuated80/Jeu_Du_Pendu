namespace Jeu_Du_Pendu // En mettant le même namespace que dans Program.cs, pas besoin d'importer la classe :-)
{
    public class Pictogramme
    {
        // Liste des étapes du pendu  
        private List<string> MonPendu;

        public Pictogramme()
        {
            // Initialiser les étapes du pendu  
            MonPendu = new List<string>
        {
              @"
                -----
                |   |
                |   
                |  
                |  
                |  
             ___|___", // Étape 0 : Pendu complet (aucune erreur)

            @"
                -----
                |   |
                |   O  
                |  
                |  
                |  
             ___|___", // Étape 1 : Tête

            @"
                -----
                |   |
                |   O  
                |   |
                |  
                |  
             ___|___", // Étape 2 : Corps

            @"
                -----
                |   |
                |   O  
                |  /|
                |  
                |  
             ___|___", // Étape 3 : Bras gauche

            @"
                -----
                |   |
                |   O  
                |  /|\
                |  
                |  
             ___|___", // Étape 4 : Bras droit

            @"
                -----
                |   |
                |   O  
                |  /|\
                |  /
                |  
             ___|___", // Étape 5 : Jambe gauche

            @"
                -----
                |   |
                |   O  
                |  /|\
                |  / \
                |  
             ___|___"  // Étape 6 : Jambe droite (perte)
        };
        }

        // Afficher l'étape actuelle du pendu  
        public void AfficherEtape(int etape)
        {
            if (etape < 0 || etape > 6)
            {
                Console.WriteLine("Étape invalide. Veuillez choisir une étape entre 0 et 6.");
                return;
            }
          
            Console.WriteLine(MonPendu[etape]);
        }
    }
}