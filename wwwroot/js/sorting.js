class SortingVisualizer {
    constructor() {
        this.steps = [];
        this.currentStep = 0;
        this.isPlaying = false;
        this.playInterval = null;
        this.speed = 1000;
        
        this.initializeElements();
        this.bindEvents();
    }
    
    initializeElements() {
        this.algorithmSelect = document.getElementById('algorithm-select');
        this.arrayInput = document.getElementById('array-input');
        this.speedSlider = document.getElementById('speed-slider');
        this.startBtn = document.getElementById('start-btn');
        this.pauseBtn = document.getElementById('pause-btn');
        this.resetBtn = document.getElementById('reset-btn');
        this.prevStepBtn = document.getElementById('prev-step');
        this.nextStepBtn = document.getElementById('next-step');
        this.arrayVisualization = document.getElementById('array-visualization');
        this.stepDescription = document.getElementById('step-description');
        this.stepText = document.getElementById('step-text');
        this.progressBar = document.getElementById('progress-bar');
        this.progressFill = document.getElementById('progress-fill');
        this.algorithmInfo = document.getElementById('algorithm-info');
        this.algorithmName = document.getElementById('algorithm-name');
        this.algorithmDescription = document.getElementById('algorithm-description');
        this.timeComplexity = document.getElementById('time-complexity');
        this.spaceComplexity = document.getElementById('space-complexity');
        this.stepControls = document.getElementById('step-controls');
    }
    
    bindEvents() {
        this.startBtn.addEventListener('click', () => this.startSorting());
        this.pauseBtn.addEventListener('click', () => this.pauseSorting());
        this.resetBtn.addEventListener('click', () => this.resetVisualization());
        this.prevStepBtn.addEventListener('click', () => this.previousStep());
        this.nextStepBtn.addEventListener('click', () => this.nextStep());
        this.speedSlider.addEventListener('input', (e) => this.speed = parseInt(e.target.value));
        this.arrayInput.addEventListener('input', () => this.resetVisualization());
        this.algorithmSelect.addEventListener('change', () => this.resetVisualization());
        
        document.addEventListener('keydown', (e) => {
            if (e.code === 'Space' && !e.target.matches('input, textarea')) {
                e.preventDefault();
                this.isPlaying ? this.pauseSorting() : this.startSorting();
            } else if (e.code === 'ArrowLeft') {
                this.previousStep();
            } else if (e.code === 'ArrowRight') {
                this.nextStep();
            }
        });
    }
    
    async startSorting() {
        if (this.steps.length === 0) {
            await this.loadSortingSteps();
        }
        
        if (this.steps.length === 0) return;
        
        this.isPlaying = true;
        this.startBtn.classList.add('hidden');
        this.pauseBtn.classList.remove('hidden');
        this.stepControls.classList.remove('hidden');
        this.progressBar.classList.remove('hidden');
        this.stepDescription.classList.remove('hidden');
        
        this.playInterval = setInterval(() => {
            if (this.currentStep < this.steps.length) {
                this.showStep(this.currentStep);
                this.currentStep++;
                this.updateProgress();
            } else {
                this.pauseSorting();
                showNotification('Сортирането е завършено!', 'success');
            }
        }, this.speed);
    }
    
    pauseSorting() {
        this.isPlaying = false;
        this.startBtn.classList.remove('hidden');
        this.pauseBtn.classList.add('hidden');
        
        if (this.playInterval) {
            clearInterval(this.playInterval);
            this.playInterval = null;
        }
    }
    
    resetVisualization() {
        this.pauseSorting();
        this.steps = [];
        this.currentStep = 0;
        this.arrayVisualization.innerHTML = '<div class=\"text-gray-500\">Въведете масив и изберете алгоритъм</div>';
        this.stepDescription.classList.add('hidden');
        this.progressBar.classList.add('hidden');
        this.stepControls.classList.add('hidden');
        this.algorithmInfo.classList.add('hidden');
        this.updateProgress();
    }
    
    async loadSortingSteps() {
        const algorithm = this.algorithmSelect.value;
        const arrayText = this.arrayInput.value.trim();
        
        if (!algorithm || !arrayText) {
            showNotification('Моля изберете алгоритъм и въведете масив', 'warning');
            return;
        }
        
        try {
            const array = arrayText.split(',').map(n => parseInt(n.trim())).filter(n => !isNaN(n));
            
            if (array.length === 0) {
                showNotification('Моля въведете валидни числа', 'error');
                return;
            }
            
            if (array.length > 20) {
                showNotification('Масивът не може да има повече от 20 елемента', 'warning');
                return;
            }
            
            const response = await fetch('/Home/GetSortingSteps', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    algorithm: algorithm,
                    array: array
                })
            });
            
            if (!response.ok) {
                throw new Error('Грешка при зареждане на стъпките');
            }
            
            const result = await response.json();
            this.steps = result.steps;
            this.showAlgorithmInfo(result);
            this.showStep(0);
            
        } catch (error) {
            console.error('Error loading sorting steps:', error);
            showNotification('Грешка при зареждане на алгоритъма', 'error');
        }
    }
    
    showAlgorithmInfo(algorithm) {
        this.algorithmName.textContent = algorithm.name;
        this.algorithmDescription.textContent = algorithm.description;
        this.timeComplexity.textContent = algorithm.timeComplexity;
        this.spaceComplexity.textContent = algorithm.spaceComplexity;
        this.algorithmInfo.classList.remove('hidden');
    }
    
    showStep(stepIndex) {
        if (stepIndex < 0 || stepIndex >= this.steps.length) return;
        
        const step = this.steps[stepIndex];
        this.renderArray(step.array, step.indices, step.highlight);
        this.stepText.textContent = step.description;
        this.updateProgress();
    }
    
    renderArray(array, indices = [], highlight = '') {
        this.arrayVisualization.innerHTML = '';
        
        if (!array || array.length === 0) {
            this.arrayVisualization.innerHTML = '<div class=\"text-gray-500\">Няма данни за показване</div>';
            return;
        }
        
        const container = document.createElement('div');
        container.className = 'flex justify-center items-end space-x-2 min-h-32';
        
        array.forEach((value, index) => {
            const element = document.createElement('div');
            element.className = 'array-item';
            element.textContent = value;
            
            // Add height based on value for better visualization
            const height = Math.max(32, (value / Math.max(...array)) * 100);
            element.style.height = `${height}px`;
            
            if (indices && indices.includes(index)) {
                element.classList.add(highlight || 'current');
            }
            
            container.appendChild(element);
        });
        
        this.arrayVisualization.appendChild(container);
    }
    
    nextStep() {
        if (this.currentStep < this.steps.length) {
            this.showStep(this.currentStep);
            this.currentStep++;
        }
    }
    
    previousStep() {
        if (this.currentStep > 0) {
            this.currentStep--;
            this.showStep(this.currentStep);
        }
    }
    
    updateProgress() {
        const progress = this.steps.length > 0 ? (this.currentStep / this.steps.length) * 100 : 0;
        this.progressFill.style.width = `${progress}%`;
        
        // Update step controls
        this.prevStepBtn.disabled = this.currentStep === 0;
        this.nextStepBtn.disabled = this.currentStep >= this.steps.length;
        
        if (this.prevStepBtn.disabled) {
            this.prevStepBtn.classList.add('opacity-50', 'cursor-not-allowed');
        } else {
            this.prevStepBtn.classList.remove('opacity-50', 'cursor-not-allowed');
        }
        
        if (this.nextStepBtn.disabled) {
            this.nextStepBtn.classList.add('opacity-50', 'cursor-not-allowed');
        } else {
            this.nextStepBtn.classList.remove('opacity-50', 'cursor-not-allowed');
        }
    }
}

// Initialize the visualizer when the page loads
document.addEventListener('DOMContentLoaded', () => {
    const visualizer = new SortingVisualizer();
    window.visualizer = visualizer; // Make it globally accessible for debugging
});

// Helper function for loading predefined arrays
function loadArray(array) {
    const input = document.getElementById('array-input');
    if (input) {
        input.value = array.join(',');
        // Trigger the input event to reset visualization
        input.dispatchEvent(new Event('input'));
    }
}