export const Governorates = [
  'Cairo',
  'Giza',
  'Alexandria',
  'Dakahlia',
  'Red Sea',
  'Beheira',
  'Fayoum',
  'Gharbiya',
  'Ismailia',
  'Menofia'
  // Add more governorates as needed
];

export const Cities: Record<string, string[]> = {
  'Cairo': ['Nasr City', 'Maadi', 'Downtown', 'Heliopolis', 'Zamalek'],
  'Giza': ['Dokki', 'Mohandessin', '6 October', 'Haram', 'Agouza'],
  'Alexandria': ['Miami', 'Stanley', 'Sidi Gaber', 'Raml Station'],
  // Add more cities for each governorate
};

export const Districts: Record<string, Record<string, string[]>> = {
  'Cairo': {
    'Nasr City': ['District 1', 'District 2', 'District 3'],
    'Maadi': ['Degla', 'Sarayat', 'Zahraa'],
    // Add more districts for each city
  },
  'Giza': {
    'Dokki': ['Street 9', 'Street 10', 'Nile Street'],
    'Mohandessin': ['Gameat Street', 'Lebanon Square'],
    // Add more districts for each city
  },
  // Add more governorates with their cities and districts
};