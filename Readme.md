Ce programme consiste à réaliser une sorte de Dex destiné à collectionner un ensemble de Champions dans l'univers de League Of Legends - Wild Rift. Il est capable de scrapper depuis le site officiel de [Wild Rift](https://wildrift.leagueoflegends.com/fr-fr/champions/) afin de recueillir les informations telles que le nom, les compétences, le(s) rôles et le niveau de difficulté pour tous les Champions à jour dans un fichier Json. Une fois d'avoir la base de données, il est possible de le charger dans le programme, de rechercher un ou plusieurs Champions selon le nom, le rôle ou le niveau de difficulté, d'ajouter d'autres Champions, de mettre à jour les informations d'un Champion existant, de sauvegarder les informations mises à jour en format de Json ou de txt.

La base de données est un fichier Json constitué des informations de Champion scrappées du site officiel de [Wild Rift](https://wildrift.leagueoflegends.com/fr-fr/champions/). Le scrapping est parti d'un fichier txt qui est une liste de noms de Champions, qui est utilisée dans le programme pour générer automatiquement les liens URLs pour scrapper. Il n'y a pas de pré-traitement particulier.

L'endroit de la base de données : 
    Data/ScrappedChampions.json

Liste de Rôles : 
    MAGE
    ASSASSIN
    TIREUR
    TANK
    COMBATTANT
    SUPPORT

Liste de niveaux de Difficultés : 
    FAIBLE
    MODÉRÉE
    ÉLEVÉE

⚠️ Attention : Le séparateur entre chaque suite de textes entrée doit être la tabulation
Commandes : 

    -h : 
        Comportement : Afficher la documentation
        Arguments nécessaires :
            RIEN
        Arguments optionnels : 
            RIEN

    add : 
        Comportement : Ajouter un Champion désigné. Si un Champion avec le même nom existe déjà dans le ChampionDex, son entrée sera réécrite.
        Arguments nécessaires : 
            1. Nom du Champion
            2. Compétence 1 du Champion
            3. Compétence 2 du Champion
            4. Compétence 3 du Champion
            5. Compétence 4 du Champion
            6. Rôle du Champion
            7. Niveau de difficulté du Champion
            Comme le séparateur est la tabulation, il est possible de mettre l'espace ou la ponctuation pour chaque argument
    
    exit : 
        Comportement : Terminer le programme
        Arguments nécessaires :
            RIEN
        Arguments optionnels : 
            RIEN

    override : 
        Comportement : Mettre à jour les informations d'un Champion. Si le Champion n'exite pas encore dans le ChampionDex, une nouvelle entrée y sera créée avec les informations fournies
        Arguments nécessaires : 
            1. Nom du Champion
            2. Compétence 1 du Champion
            3. Compétence 2 du Champion
            4. Compétence 3 du Champion
            5. Compétence 4 du Champion
            6. Rôle du Champion
            7. Niveau de difficulté du Champion
            Comme le séparateur est la tabulation, il est possible de mettre l'espace ou la ponctuation pour chaque argument

    load : 
        Comportement : Charger un fichier Json qui correspond au format attendu d'un ChampionDex. Les Champions existent déjà dans le ChampionDex actuel du programme seront sautés et les nouveaux Champions seront ajoutés dans le ChampionDex actuel.
        Arguments nécessaires : 
            1. Le chemin du fichier Json à charger

    loadtxt :
        Comportement : Charger un fichier txt qui contient les information de Champions. Les Champions existent déjà dans le ChampionDex actuel du programme seront sautés et les nouveaux Champions seront ajoutés dans le ChampionDex actuel.
        Arguments nécessaires : 
            1. Le chemin du fichier Json à charger
    
    save : 
        Comportement : Sauvegarde le ChampionDex en format Json dans un endroit spécifique. Si le chemin est le même que la base de données, le sauvegarde ne sera pas effectué afin de laisser la base de données intacte.
        Arguments nécessaires : 
            1. Le chemin à sauvegarder le fichier Json
    
    savetxt : 
        Comportement : Sauvegarde le ChampionDex en format Txt dans un endroit spécifique
        Arguments nécessaires : 
            1. Le chemin à sauvegarder le fichier Txt
    
    scrape : 
        Comportement : Scrapper et générer la base de données depuis le site officiel de Wild Rift
        Arguments nécessaires : 
            1. Le chemin du fichier TXT qui contient la liste des noms de Champions
    
    search : 
        Comportement : Chercher un ou plusieurs Champions et afficher ses ou leurs informations
        Arguments nécessaires :
            1. Le type de recherche : le type doit être l'un parmi "name", "role", "difficulty"
            2. Le motif de recherche : 
                Type "name" : Le nom d'un Champion
                Type "role" : L'un des rôles 
                Type "difficulty" : L'un des niveaux de difficulté