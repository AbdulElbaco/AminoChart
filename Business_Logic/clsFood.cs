using Data_Access;
using System.Security.Cryptography.X509Certificates;

namespace Business_Logic
{
    struct stAA
    {
        public string Name;
        public float Percentage_AA2TAAs;
    }
    struct stAAProfile
    {
        public stAA Alanine;           // Ala, A
        public stAA Arginine;          // Arg, R
        public stAA Asparagine;        // Asn, N
        public stAA Aspartic_Acid;     // Asp, D
        public stAA Cysteine;          // Cys, C (not "Cystine", which is a dimer of Cysteine)
        public stAA Glutamic_Acid;     // Glu, E
        public stAA Glutamine;         // Gln, Q
        public stAA Glycine;           // Gly, G
        public stAA Histidine;         // His, H
        public stAA Isoleucine;        // Ile, I
        public stAA Leucine;           // Leu, L
        public stAA Lysine;            // Lys, K (missing in your original list)
        public stAA Methionine;        // Met, M
        public stAA Phenylalanine;    // Phe, F
        public stAA Proline;           // Pro, P
        public stAA Serine;            // Ser, S
        public stAA Selenocysteine;    // Sec, U (rare, found in some proteins)
        public stAA Threonine;         // Thr, T
        public stAA Tryptophan;       // Trp, W
        public stAA Tyrosine;          // Tyr, Y
        public stAA Valine;            // Val, V
    };
    public struct stIdealProfile
    {
        // Essential Amino Acids (EAAs)
        public float Leucine;      // BCAA - Primary MPS trigger
        public float Lysine;       // Often limiting in plant proteins
        public float Valine;       // BCAA
        public float Isoleucine;   // BCAA
        public float Threonine;
        public float Phenylalanine;
        public float Methionine;   // Sulfur AA (paired with Cysteine)
        public float Histidine;
        public float Tryptophan;   // Typically lowest

        // Conditionally Essential
        public float Arginine;     // Nitric oxide synthesis
        public float Glutamine;    // Muscle hydration/recovery
        public float Tyrosine;     // Precursor to neurotransmitters

        // Non-Essential (but functionally important)
        public float Alanine;
        public float Aspartic_Acid;
        public float Glutamic_Acid;
        public float Glycine;
        public float Proline;
        public float Serine;
        public float Cysteine;     // Sulfur AA (paired with Methionine)
        public float Asparagine;
        public float Selenocysteine; // Trace element

        public stIdealProfile()
        {
                // =====================
                // SCIENCE-BASED RATIOS
                // =====================
                // 1. EAAs: 45.3% of total (FAO/WHO muscle composition baseline)
                // 2. BCAA Ratio: Leucine 2.5× higher than standard muscle protein
                // 3. Leucine: 12% of total AAs (Wolfe et al. 2017 upper threshold)
                // 4. Methionine+Cysteine: 4.3% (FAO/WHO sulfur AA requirement)
                // 5. Lysine: Elevated for plant protein compensation

                // EAAs (Total 45.3%)
                Leucine = 12.0f;    // ↑ 40% vs muscle protein (8.6% → 12%)
                Lysine = 8.2f;      // Maintained for peptide bonding
                Valine = 6.0f;      // BCAA (↓ from 6.8% to balance leucine)
                Isoleucine = 4.5f;   // BCAA (Churchward-Venne ratio)
                Threonine = 4.0f;
                Phenylalanine = 3.8f;
                Methionine = 2.0f;   // Sulfur AA (↓ paired with cysteine)
                Histidine = 2.2f;
                Tryptophan = 1.6f;   // ↑ for neurotransmitter synthesis

                // Conditionally Essential (15.4%)
                Arginine = 5.0f;     // ↑↑ Nitric oxide boost (standard: 4.7%)
                Glutamine = 6.0f;    // ↑↑ Muscle hydration (standard: 4.4%)
                Tyrosine = 4.4f;     // ↑ Dopamine/adrenaline precursor

                // Non-Essential (39.3%)
                Cysteine = 2.7f;     // Sulfur AA (Methionine+Cysteine=4.3%)
                Alanine = 5.9f;      // Glucose precursor
                Aspartic_Acid = 5.6f;
                Glutamic_Acid = 7.9f; // Neurotransmitter substrate
                Glycine = 4.4f;       // Collagen synthesis
                Proline = 4.9f;       // Connective tissue
                Serine = 4.2f;
                Asparagine = 3.6f;
                Selenocysteine = 0.1f;
        }
    }

    public class Calculations_Config
    {
        static public IReadOnlyDictionary<string, float> Weights = new Dictionary<string, float>
        {
            // Essential AAs (87%)
            ["Leucine"] = 0.35f,
            ["Lysine"] = 0.15f,
            ["Valine"] = 0.06f,
            ["Isoleucine"] = 0.05f,
            ["Methionine"] = 0.08f,
            ["Cysteine"] = 0.02f,
            ["Threonine"] = 0.06f,
            ["Phenylalanine"] = 0.05f,
            ["Histidine"] = 0.03f,
            ["Tryptophan"] = 0.02f,

            // Conditionally Essential (13%)
            ["Arginine"] = 0.05f,
            ["Glutamine"] = 0.05f,
            ["Tyrosine"] = 0.03f,

            // Non-essentials explicitly zero-weighted
            ["Alanine"] = 0f,
            ["Glycine"] = 0f,
            ["Proline"] = 0f,
            ["Serine"] = 0f,
            ["Aspartic Acid"] = 0f,
            ["Glutamic Acid"] = 0f,
            ["Asparagine"] = 0f,
            ["Selenocysteine"] = 0f
        };
    }
    public class clsFood
    {
        public string Name;
        private float Similarity_Score;
        private stAAProfile _AAs_Profile;
        public float Protein_Percentage;

        public clsFood(string Name)
        {
            // Initialize the food object with the name, the EAA profile and Protein_Percentage
            this.Name = Name;
            this.Similarity_Score = 0;
            this.Protein_Percentage = clsFoodData.Get_Protein_Percentage(Name);

            // Initialize the AA profile from database
            _AAs_Profile.Alanine.Name = "Alanine";
            _AAs_Profile.Arginine.Name = "Arginine";
            _AAs_Profile.Asparagine.Name = "Asparagine";
            _AAs_Profile.Aspartic_Acid.Name = "Aspartic Acid";
            _AAs_Profile.Cysteine.Name = "Cysteine";
            _AAs_Profile.Glycine.Name = "Glycine";
            _AAs_Profile.Glutamic_Acid.Name = "Glutamic Acid";
            _AAs_Profile.Glutamine.Name = "Glutamine";
            _AAs_Profile.Histidine.Name = "Histidine";
            _AAs_Profile.Isoleucine.Name = "Isoleucine";
            _AAs_Profile.Leucine.Name = "Leucine";
            _AAs_Profile.Lysine.Name = "Lysine";
            _AAs_Profile.Methionine.Name = "Methionine";
            _AAs_Profile.Phenylalanine.Name = "Phenylalanine";
            _AAs_Profile.Proline.Name = "Proline";
            _AAs_Profile.Selenocysteine.Name = "Selenocysteine";
            _AAs_Profile.Serine.Name = "Serine";
            _AAs_Profile.Threonine.Name = "Threonine";
            _AAs_Profile.Tryptophan.Name = "Tryptophan";
            _AAs_Profile.Tyrosine.Name = "Tyrosine";
            _AAs_Profile.Valine.Name = "Valine";


            bool isFound = clsFoodData.Get_AAs_Profile(Name,
                ref _AAs_Profile.Alanine.Percentage_AA2TAAs,
                ref _AAs_Profile.Arginine.Percentage_AA2TAAs,
                ref _AAs_Profile.Asparagine.Percentage_AA2TAAs,
                ref _AAs_Profile.Aspartic_Acid.Percentage_AA2TAAs,
                ref _AAs_Profile.Cysteine.Percentage_AA2TAAs,
                ref _AAs_Profile.Glutamic_Acid.Percentage_AA2TAAs,
                ref _AAs_Profile.Glutamine.Percentage_AA2TAAs,
                ref _AAs_Profile.Glycine.Percentage_AA2TAAs,
                ref _AAs_Profile.Histidine.Percentage_AA2TAAs,
                ref _AAs_Profile.Isoleucine.Percentage_AA2TAAs,
                ref _AAs_Profile.Leucine.Percentage_AA2TAAs,
                ref _AAs_Profile.Lysine.Percentage_AA2TAAs,
                ref _AAs_Profile.Methionine.Percentage_AA2TAAs,
                ref _AAs_Profile.Phenylalanine.Percentage_AA2TAAs,
                ref _AAs_Profile.Proline.Percentage_AA2TAAs,
                ref _AAs_Profile.Serine.Percentage_AA2TAAs,
                ref _AAs_Profile.Selenocysteine.Percentage_AA2TAAs,
                ref _AAs_Profile.Threonine.Percentage_AA2TAAs,
                ref _AAs_Profile.Tryptophan.Percentage_AA2TAAs,
                ref _AAs_Profile.Tyrosine.Percentage_AA2TAAs,
                ref _AAs_Profile.Valine.Percentage_AA2TAAs);
            Similarity_Score = -1;
        }
        public float Get_Similarity_Score()
        {
            // Return cached score if already calculated
            if (Similarity_Score != -1)
            {
                return Similarity_Score;
            }

            float totalScore = 0f;
            stIdealProfile idealProfile = new stIdealProfile(); // Uses science-based defaults

            // Calculate weighted similarity for each amino acid
            foreach (var field in typeof(stAAProfile).GetFields())
            {
                string aaName = field.Name;

                // Get the amino acid values from both profiles
                stAA foodAA = (stAA)field.GetValue(_AAs_Profile);
                float idealValue = (float)typeof(stIdealProfile).GetField(aaName).GetValue(idealProfile);

                // Skip if this AA has no weight (non-essentials)
                if (!Calculations_Config.Weights.TryGetValue(aaName, out float weight) || weight <= 0)
                {
                    continue;
                }

                // Calculate contribution to score (penalizing deviations from ideal)
                float deviation = Math.Abs(idealValue - foodAA.Percentage_AA2TAAs) / idealValue;
                float partialScore = weight * (1 - deviation);
                totalScore += partialScore;
            }

            // Convert to percentage (weights already sum to 1.0)
            Similarity_Score = Math.Clamp(totalScore * 100f, 0f, 100f);
            return Similarity_Score;
        }
    }
}