using System.Data;
using System.Globalization;
using System.Text; // Importer les manipulations avancées sur du texte.

namespace Jeu_Du_Pendu
{
    internal class Program
    {
        private static string[] Charger_Le_Fichier(string NomDuFichier)
        {
            try
            {
                if (!File.Exists(NomDuFichier))
                {
                    Console.WriteLine("Oups, votre fichier est introuvable !");
                    return new string[0]; // Retourne un tableau vide  
                }

                else
                {
                    // Lire toutes les lignes du fichier et les stocker dans un tableau  
                    String[] MaListeDeMotsNonFormatee = File.ReadAllLines(NomDuFichier);

                    // Vérification si le tableau est vide  
                    if (MaListeDeMotsNonFormatee.Length == 0)
                    {
                        Console.WriteLine("Oups, il semblerait que votre fichier soit vide !");
                        return new string[0]; // Retourne un tableau vide  
                    }

                    // Exemple d'utilisation  
                    for (int i = 0; i < MaListeDeMotsNonFormatee.Length; i++)
                    {
                        Console.WriteLine($"Mot [{i}] = {MaListeDeMotsNonFormatee[i]}");
                    }
                    return MaListeDeMotsNonFormatee;
                } 
            }

            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);
                // Retourne un tableau vide en cas d'erreur  
                return new string[0];
            }
        }
        private static string Piocher_Aleatoirement_Un_Mot(string [] tabMots)
        {
            Random alea = new Random();
            int nbAlea = alea.Next(tabMots.Length); // Simule un chiffre entre 0 et le nombre d'éléments de tabMots - 1
            String motChoisi = tabMots[nbAlea]; // On stocke l'élément pioché dans le tableau. 
            return motChoisi;
        }
        private static string NettoyerMot(string mot)
        {
            // Mettre en majuscules  
            mot = mot.ToUpperInvariant();

            // Enlever les accents  
            mot = SupprimerAccents(mot);

            // Enlever les espaces  
            mot = mot.Replace(" ", "");

            return mot;
        }
        private static char NettoyerLettre(char lettre)
        {
            // Convertir en majuscule  
            char majuscule = char.ToUpper(lettre);

            // Retirer les accents  
            string normalizedString = new string(majuscule.ToString()
                .Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            // Retourner la première lettre du résultat nettoyé  
            return normalizedString.Length > 0 ? normalizedString[0] : majuscule;
        }
        private static string SupprimerAccents(string texte)
        {
            var texteNormalise = texte.Normalize(NormalizationForm.FormD);
            var constructeurString = new StringBuilder();

            foreach (var c in texteNormalise)
            {
                var categorieUnicode = CharUnicodeInfo.GetUnicodeCategory(c);
                if (categorieUnicode != UnicodeCategory.NonSpacingMark)
                {
                    constructeurString.Append(c);
                }
            }

            return constructeurString.ToString().Normalize(NormalizationForm.FormC);
        }
        private static int Initiatlisation_Jeu(string leMotAtrouver, int nbreVieRestante, string lettresProposees, char choixLettre, int tour)
        {
            int i = 0;
            int j = 0;
            bool correspondance = false;
            int lettresOk = 0;

            Console.Clear(); // On nettoie l'affichage 
            Console.WriteLine($"Tour n° {tour}");
            Console.WriteLine("================\n");
            Pictogramme pictogramme = new Pictogramme();
            pictogramme.AfficherEtape(6 - nbreVieRestante); //Permet d'afficher le pictogramme correspondant à l'étape du pendu.

            Console.WriteLine($"\n\nNbre de vies restantes : {nbreVieRestante}");
            Console.WriteLine($"Lettres déjà proposées : {lettresProposees}\n\n");

                while (i < leMotAtrouver.Length)
                {
                    if (choixLettre == leMotAtrouver[i])
                    {
                        i++;
                        correspondance = true;
                    }
                    else
                    {
                        i++;
                    }
                }

                while (j < leMotAtrouver.Length)
                {
                    if (lettresProposees.Contains(leMotAtrouver[j]))
                    {
                        Console.Write($"{leMotAtrouver[j]} ");
                        lettresOk++;
                        j++;
                    }
                
                    else
                    {
                    Console.Write("_ ");
                    j++;
                    }                    
                }

            if ((correspondance == false) && (tour > 1))
            {
                nbreVieRestante--;
                Console.Clear(); // On nettoie l'affichage 
                Console.WriteLine($"Tour n° {tour}");
                Console.WriteLine("================\n");
                pictogramme.AfficherEtape(6 - nbreVieRestante); //Permet d'afficher le pictogramme correspondant à l'étape du pendu.

                Console.WriteLine($"\n\nNbre de vies restantes : {nbreVieRestante}");
                Console.WriteLine($"Lettres déjà proposées : {lettresProposees}\n\n");

                j = 0;
                while (j < leMotAtrouver.Length)
                {
                    if (lettresProposees.Contains(leMotAtrouver[j]))
                    {
                        Console.Write($"{leMotAtrouver[j]} ");
                        lettresOk++;
                        j++;
                    }

                    else
                    {
                        Console.Write("_ ");
                        j++;
                    }
                }
                Console.WriteLine("\n\nHo, pas de chance...");
                //System.Threading.Thread.Sleep(1000);
            }

            else
            {
                if ((correspondance == true) && (tour > 1))
                {
                    Console.WriteLine("\n\nHo, bien joué !");
                    //System.Threading.Thread.Sleep(1000);
                }
            }

                if (lettresOk == leMotAtrouver.Length)
            {
                nbreVieRestante = 100; // Dans le cas où le nombre de vie atteint 100, cela signifie que nous avons gagné la partie ! 
            }

            return nbreVieRestante;
        }
        private static char Choix_Lettre(string lettreDejaChoisie)
        {
            while(true)
            {
                try
                {
                    Console.Write("\n\nChoisissez une lettre pour trouver le mot caché : ");
                    char choixLettre = char.Parse(Console.ReadLine());
                    char lettreNettoyee = NettoyerLettre(choixLettre);

                    if (lettreDejaChoisie.Contains(lettreNettoyee))
                    {
                        Console.WriteLine("\nVous devez rentrer une lettre qui n'a pas encore été choisie !");
                    }
                                      
                    else
                    {
                        // Vérification si le caractère est une lettre  
                        if (!char.IsLetter(lettreNettoyee))
                        {
                            throw new ArgumentException("\nL'entrée doit être une lettre appartenant à l'alphabet français...\n" +
                                "En effet, les caractères spéciaux et les chiffres ne sont pas autorisés !");
                        }

                        return lettreNettoyee;
                    }    
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nVous devez rentrer une lettre valide !");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex) // Bloc générique pour d'autres exceptions  
                {
                    Console.WriteLine($"\nErreur inattendue : {ex.Message}");
                }
            }
        }
        static void Main(string[] args)
        {
            char rejouer = 'Z';

            while (rejouer != 'N')
            {
                Console.Clear();
                Console.WriteLine("Bienvenue Dans le Programme du Jeu du Pendu !");
                Console.WriteLine("_____________________________________________");
                System.Threading.Thread.Sleep(1500);
                string[] motsCharges = Charger_Le_Fichier("MonFichier.txt");
                int nbreVieRestante = 6;
                string lettresProposees = "";
                char choixLettre = ' ';//Par défaut, lorsque l'utilisateur n'a pas encore choisi de lettre, la variable ne contient qu'un espace.
                bool gagne = false;
                bool perdu = false;
                int tour = 0;
                

                if (motsCharges.Length == 0)
                {
                    Console.WriteLine("\nDésolé, comme le fichier n'est pas lisible, je ne peux poursuivre le programme !");
                    return; // Arrête l'exécution du programme si le fichier n'est pas lisible  
                }

                else
                {
                    String motChoisi = Piocher_Aleatoirement_Un_Mot(motsCharges);
                    if ((motChoisi.Length == 0) || (motChoisi == ""))
                    {
                        Console.WriteLine("\nUne erreur s'est produite, le mot choisi aléatoirement n'est pas valide !");
                        Console.WriteLine("\nLe programme va s'arrêter. Je vous invite à vérifier votre fichier <MonFichier.txt !>");
                        Console.WriteLine("\nIl doit être présent dans le bin et contenir au moins une valeur. Les mots doivent être alignés verticalement.");
                        return;
                    }

                    //Console.WriteLine($"\nLe mot choisi est {motChoisi}");// Pour tester le mot choisi 

                    string motChoisiEpure = NettoyerMot(motChoisi); // Nettoyer le mot choisi 
                    //Console.WriteLine($"\nLe mot épuré est {motChoisiEpure}"); // Pour tester le mot épuré de tout accent, avec majuscule et sans espace.
                    //System.Threading.Thread.Sleep(2000);

                    while ((gagne == false) && (perdu == false))
                    {
                        if (nbreVieRestante == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nDésolé, vous n'avez plus de vie, vous venez d'être pendu !");
                            Pictogramme pictogramme = new Pictogramme();
                            pictogramme.AfficherEtape(6);
                            perdu = true;
                            Console.WriteLine($"\nPour information, le mot à trouver était {motChoisi}");
                            System.Threading.Thread.Sleep(3000);
                        }
                        else
                        {
                            if (nbreVieRestante == 100)
                            {
                                Console.Clear();
                                Console.WriteLine("\nFélicitation, vous avez gagné !");
                                Console.WriteLine($"\nLe mot à trouver était bien {motChoisi}");
                                Console.WriteLine("\nVotre pendaison n'est pas pour cette partie !");
                                gagne = true;
                                System.Threading.Thread.Sleep(3000);
                            }

                            else
                            {
                                tour++;
                            
                                nbreVieRestante = Initiatlisation_Jeu(motChoisiEpure, nbreVieRestante, lettresProposees, choixLettre, tour);
                               
                                
                                if (nbreVieRestante > 0 && nbreVieRestante < 100)
                                {
                                    choixLettre = Choix_Lettre(lettresProposees); // On demande à l'utilisateur de taper une lettre.
                                    Console.WriteLine($"\nVous avez choisi la lettre {choixLettre}");
                                    System.Threading.Thread.Sleep(1000);
                                }
                                   

                                if (lettresProposees.Length == 0)
                                {
                                    lettresProposees += choixLettre;
                                }

                                else
                                {
                                    lettresProposees += ", " + choixLettre;
                                }       
                            }
                        }
                    }

                    while(true)
                    {
                        try
                        {
                            Console.Write("\n\nLa partie est maintenant terminée, voulez-vous rejouer O/N ? ");
                            rejouer = Char.ToUpper(char.Parse(Console.ReadLine())); // On force la majuscule.

                            if ((rejouer != 'O') && (rejouer != 'N'))
                            {
                                Console.WriteLine("\nOups, ce choix n'est pas valide. Vous devez répondre par O(oui) ou N(non)");
                            }
                            else
                            {
                                if (rejouer == 'O')
                                {
                                    Console.WriteLine("\nD'accord, la partie va redemarrer dans quelques secondes...");
                                    System.Threading.Thread.Sleep(3000);
                                    break; // On quitte le while pour relancer une nouvelle partie !
                                }

                                else
                                {
                                    Console.WriteLine("\nAu revoir et à bientôt !");
                                    System.Threading.Thread.Sleep(1000);
                                    return; //On quitte le programme !
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("\nOups, ce choix n'est pas valide. Vous devez répondre par O(oui) ou N(non)");
                        }
                    }
                }
            }
        }
    }
}