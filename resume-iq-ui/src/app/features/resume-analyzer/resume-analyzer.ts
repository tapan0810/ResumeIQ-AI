import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { ResumeService } from '../../core/services/resume';
import { ResumeAnalysis } from '../../core/models/resume-analysis.model';

@Component({
  selector: 'app-resume-analyzer',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './resume-analyzer.html',
  styleUrl: './resume-analyzer.scss'
})
export class ResumeAnalyzer {

  private readonly resumeService = inject(ResumeService);

  // -----------------------------
  // Component State
  // -----------------------------

  resumeText = '';

  analysis = signal<ResumeAnalysis | null>(null);

  isLoading = signal(false);

  errorMessage = signal('');

  // -----------------------------
  // Analyze Resume
  // -----------------------------

  analyzeResume(): void {

    console.log('================================');
    console.log('ANALYZE BUTTON CLICKED');
    console.log('================================');

    if (!this.resumeText.trim()) {

      this.errorMessage.set(
        'Please enter your resume text.'
      );

      return;
    }

    // Reset state

    this.analysis.set(null);

    this.errorMessage.set('');

    this.isLoading.set(true);

    console.log('Loading started');

    console.log(
      'Resume length:',
      this.resumeText.length
    );

    // -----------------------------
    // API Call
    // -----------------------------

    this.resumeService
      .analyzeResume({
        resumeText: this.resumeText
      })
      .subscribe({

        next: (response: ResumeAnalysis) => {

          console.log(
            'SUCCESS - RESPONSE RECEIVED'
          );

          console.log(
            'Response:',
            response
          );

          // Update signal

          this.analysis.set(response);

          console.log(
            'Analysis signal updated'
          );

        },

        error: (error) => {

          console.error(
            'API ERROR:',
            error
          );

          console.error(
            'Status:',
            error?.status
          );

          console.error(
            'Error body:',
            error?.error
          );

          this.errorMessage.set(
            error?.error?.message ??
            'Unable to analyze the resume. Please try again.'
          );

        },

        complete: () => {

          console.log(
            'HTTP REQUEST COMPLETED'
          );

          // Always stop loading

          this.isLoading.set(false);

          console.log(
            'Loading state:',
            this.isLoading()
          );

        }

      });
  }

  // -----------------------------
  // Reset
  // -----------------------------

  resetAnalysis(): void {

    this.analysis.set(null);

    this.errorMessage.set('');

    this.resumeText = '';

  }
}