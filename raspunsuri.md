1. Modificarea constantei MatrixMode.Projection în OpenGL afectează modul în care sunt proiectate obiectele 3D pe ecranul 2D.

-->Într-o aplicație de grafică tehnică sau desen 2D, proiecția ortografică este preferată pentru ca nu este nevoie de perspectivă.
-->Într-o aplicație 3D, schimbarea la proiecție în perspectivă oferă o vizualizare realistă a lumii 3D, unde obiectele se micșorează proporțional cu distanța față de cameră.

3. RASPUNS LA INTREBARI
  3.1.Ce este un viewport? 
---> Un viewport în OpenGL reprezintă zona de pe ecran unde se face randarea scenei. Poate fi definită de dimensiuni și poziție folosind GL.Viewport(x, y, width, height). Practic, controlează ce porțiune a ferestrei este folosită pentru a afișa grafica generată.

  3.2. Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
---> FPS indică numărul de cadre care sunt randate pe secundă. Este o măsură a performanței randării, și în contextul OpenGL, FPS reflectă cât de repede poate aplicația să actualizeze și să afișeze scenele 3D.

  3.3.Când este rulată metoda OnUpdateFrame()?
---> Metoda OnUpdateFrame() este rulată înainte de fiecare cadru și este responsabilă cu actualizarea logicii scenei (mișcarea obiectelor, calculul coliziunilor, etc.), fără să aibă legătură directă cu randarea graficii.

  3.4.Ce este modul imediat de randare?
---> Modul imediat de randare este o metodă veche de randare în OpenGL, în care comenzile de randare sunt trimise direct către GPU în timp real, pentru fiecare cadru. Aceasta implică apeluri la funcții precum glBegin() și glEnd() și este mai puțin eficientă decât metodele moderne de randare bazate pe shadere și bufferi.

  3.5.Care este ultima versiune de OpenGL care acceptă modul imediat?
---> OpenGL 3.0 a fost ultima versiune care a suportat modul imediat în mod oficial. În versiunile ulterioare, acest mod a fost eliminat în contextul "Core Profile", dar poate fi accesibil în "Compatibility Profile".

  3.6.Când este rulată metoda OnRenderFrame()?
---> Când este rulată metoda OnRenderFrame()? OnRenderFrame() este rulată la fiecare cadru și este responsabilă pentru randarea graficii. În cadrul acestei metode, se fac apeluri OpenGL pentru a desena obiectele în fereastră.

  3.7.De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
---> Metoda OnResize() este folosită pentru a ajusta viewport-ul și proiecția atunci când dimensiunea ferestrei este schimbată. Fără aceasta, imaginea ar putea fi deformată sau afișată incorect dacă utilizatorul redimensionează fereastra.

  3.8.Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?
---> Metoda CreatePerspectiveFieldOfView() este folosită pentru a configura o proiecție în perspectivă. Parametrii includ:
  *fieldOfViewY: unghiul câmpului vizual pe verticală, măsurat în radiani (domeniul tipic este între 30° și 90°).
  *aspectRatio: raportul de aspect al ferestrei (width/height).
  *nearPlane: distanța de la cameră până la planul apropiat (minim pozitiv).
  *farPlane: distanța până la planul îndepărtat (trebuie să fie mai mare decât nearPlane).
Domeniul de valori pentru acești parametri este dependent de specificațiile scenei și ale obiectelor randate.



### TEMA LAB 3

1. Ordinea de desenare a vertexurilor:
   - Triunghiuri: Anti-orar (pentru a fi considerate front-facing).
   - Axele de coordonate pot fi desenate folosind un singur apel GL.Begin() cu GL.Begin(PrimitiveType.Lines).

2. Anti-aliasing:
   Anti-aliasing este o tehnică utilizată pentru a reduce efectul de "jaggies" sau margini zimțate pe contururile obiectelor grafice. Aceasta funcționează prin amestecarea culorilor pixelilor de margine, oferind o tranziție mai lină între culori.

3. Efectul rulării comenzilor:
   - GL.LineWidth(float): Modifică lățimea liniei desenate. Funcționează în interiorul unei zone GL.Begin().
   - GL.PointSize(float): Modifică dimensiunea punctului desenat. Funcționează de asemenea în interiorul unei zone GL.Begin().

4. Răspunsuri la întrebările specifice:
   - LineLoop: Desenează linii între vertexuri, conectând ultimul vertex cu primul, formând un poligon.
   - LineStrip: Desenează linii între vertexuri, conectând fiecare vertex la următorul, fără a închide figura.
   - TriangleFan: Desenează triunghiuri folosind un vertex central, formând triunghiuri succesive în jurul acestuia.
   - TriangleStrip: Desenează triunghiuri consecutive, folosind ultimele două vertexuri pentru a forma fiecare triunghi nou.

5. Proiect elementar:
   Creați un proiect nou în care să includeți un exemplu simplu de desenare a unor primitive folosind OpenGL, respectând setările corecte pentru viewport.

6. Importanța culorilor diferite în obiecte 3D:
   Culorile diferite permit crearea de efecte de adâncime și volum, făcând obiectele mai realiste și mai vizibile. Gradientele ajută la percepția formei și detaliilor.

7. Gradient de culoare:
   Un gradient de culoare reprezintă o tranziție lină între două sau mai multe culori. În OpenGL, acesta se obține setând culori diferite pentru fiecare vertex al unei primitive.

8. Culoarea triunghiului:
   Când se modifică culoarea triunghiului între valorile minime și maxime pentru fiecare canal, efectul utilizării canalului de transparență poate duce la crearea unor efecte vizuale de suprapunere sau transparență parțială.

9. Manipularea valorilor RGB:
   Modificarea valorilor RGB pentru fiecare vertex va permite obținerea unor efecte de gradient și diversitate vizuală, iar valorile RGB pot fi afișate în consolă pentru a verifica modificările.

10. Efectul culorilor diferite pe vertexuri:
   Utilizarea unor culori diferite pentru fiecare vertex atunci când se desenează o linie sau un triunghi în modul strip va crea un gradient de culoare, adăugând complexitate vizuală și estetică scenei.

