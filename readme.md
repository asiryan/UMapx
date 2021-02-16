<p align="center"><img width="25%" src="docs/umapxnet_big.png" /></p>

**UMapx.NET** is a cross-platform .NET library for digital signal processing.  

# UMapx.NET
### Contains ready-made math tools:
* color spaces and their transformations,
* real and complex algebra,
* statistical distributions,
* special math functions,
* digital response filters,
* discrete orthogonal transforms and more.

### Suitable for a wide range of tasks:
* symbolic and graphical visualization of data,
* functional, vector and matrix analysis,
* interpolation, approximation and optimization of functions,
* numerical differentiation and integration,
* solving equations,
* matrix factorization.

### Includes special toolboxes:
* **Wavelet Toolbox**. Provides wide functionality for the study of discrete and continuous wavelets. The toolbox also includes algorithms for discrete one-dimensional and two-dimensional wavelet transforms of real and complex signals.
* **Window Toolbox**. Includes a set of tools to synthesizing and orthogonalizing window functions. It implements discrete short-time Fourier and Weyl-Heisenberg transforms ([Gabor analysis](https://github.com/asiryan/Weyl-Heisenberg-Toolbox)) for real and complex signals.
* **Image Processing Toolbox**. Contains efficient algorithms to processing, correcting and analyzing 32-bit images.
* **Video Processing Toolbox**. Includes a set of tools to video streaming and processing.

# Supported types
| Object | Type | Bits |
|-------------|-------------|-------------|
| Array | float, Complex | 32 |
| Image | Format32bppArgb | 32 |
| Video | Not specified | Not specified |

# Installation
You can build **UMapx.NET** from sources or install to your own project using nuget package manager.
| Version | Specification | OS | Platform | Download | Package |
|-------------|-------------|-------------|-------------|--------------|--------------|
| 5.0.0.2 | .NET Standard 2.0 | Cross-platform | AnyCPU | [Release](https://github.com/asiryan/UMapx.NET/releases/) | [NuGet](https://www.nuget.org/packages/UMapx/) |

# Namespaces
```c#
using UMapx.Analysis;
using UMapx.Colorspace;
using UMapx.Core;
using UMapx.Decomposition;
using UMapx.Distribution;
using UMapx.Imaging;
using UMapx.Response;
using UMapx.Transform;
using UMapx.Video;
using UMapx.Visualization;
using UMapx.Wavelet;
using UMapx.Window;
```

# Examples of usage
* [Local Laplacian filters](https://github.com/asiryan/Local-Laplacian-filters) - NET Framework desktop application for HDR imaging.
* [Portrait mode effect](https://github.com/asiryan/Portrait-mode) - High quality implementation of the portrait mode effect using Neural Networks.

# Relation to other frameworks
**UMapx.NET** is based on several separate frameworks (AForge.NET, Accord.NET, Alglib and etc). Some functions have been ported from other programming languages and their toolboxes and libraries (Fortran, MATLAB, C++, Python). The purpose of this generalization was the declarative understanding of algorithms of digital signal processing and its optimization and performance improvement. **UMapx.NET** is faster than AForge.NET and Accord.NET in signal processing tasks and contains a larger set of functions for matrix analysis, linear algebra and functional analysis.

# License
**MIT**  

# References
A full list of references is given in a separate [file](docs/references.pdf).  
